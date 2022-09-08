using MegafonATS.Client.Results;
using MegafonATS.Models;
using MegafonATS.Models.Client;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MegafonATS.Client
{
    public class MegafonAtsClientBase
    {
        readonly string baseUri = "https://{this.options.Name}.megapbx.ru/sys/crm_api.wcgp";

        readonly HttpClient httpClient;
        readonly MegafonAtsOptions options;
        readonly static JsonSerializerOptions jsonSerializerOptions;

        static MegafonAtsClientBase()
        {
            jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public MegafonAtsClientBase(HttpClient httpClient, MegafonAtsOptions options)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.options = options ?? throw new ArgumentNullException(nameof(options));

            if (options.Name == null)
                throw new ArgumentNullException(nameof(options.Name));
            if (options.Key == null)
                throw new ArgumentNullException(nameof(options.Key));

            baseUri = $"https://{this.options.Name}.megapbx.ru/crmapi/v1";
            this.httpClient.BaseAddress = new Uri($"https://{this.options.Name}.megapbx.ru/sys/crm_api.wcgp");
        }

        #region Helpers

        enum AtsCommand
        {
            accounts,
            groups,
            makeCall,
            history,
            subscribeOnCalls,
            subscriptionStatus,
            set_dnd,
            get_dnd
        }

        protected async Task<ClientResult<TResponse>> ExecuteGetAsync<TResponse>(string method, CancellationToken cancellationToken)
        {
            var requestUri = new Uri(baseUri + method);
            using var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            request.Headers.Add("X-API-KEY", options.Key);
            using var response = await httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return ClientResult<TResponse>.SetError(response.StatusCode.ToString());
            string data = await response.Content.ReadAsStringAsync();
            TResponse deserializeResponse = await response.Content.ReadFromJsonAsync<TResponse>(jsonSerializerOptions, cancellationToken);
            return ClientResult<TResponse>.Success(deserializeResponse);
        }



        async Task<HttpResponseMessage> GetResponseAsync(AtsCommand command, Dictionary<string, string> parameters, CancellationToken cancellationToken)
        {
            var values = MakeRequestBody(command, parameters);
            return await httpClient.PostAsync(httpClient.BaseAddress, values, cancellationToken);
        }

        async Task<ClientResult<TResponse>> ProcessResponseAsync<TResponse>(AtsCommand command, Dictionary<string, string> parameters, CancellationToken cancellationToken) where TResponse : class
        {
            var response = await GetResponseAsync(command, parameters, cancellationToken);

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    {
                        if (typeof(TResponse) == typeof(IEnumerable<CallModel>))
                        {
                            var str = await response.Content.ReadAsStringAsync();
                            var result = ClientResult<IEnumerable<CallModel>>.Success(CreateListOfCalls(str));


                            return (ClientResult<TResponse>)Convert.ChangeType(result, typeof(ClientResult<TResponse>));
                        }
                        else
                        {
                            var result = await response.Content.ReadFromJsonAsync<TResponse>(jsonSerializerOptions, cancellationToken);
                            return ClientResult<TResponse>.Success(result);
                        }

                    }
                case System.Net.HttpStatusCode.BadRequest:
                    {
                        var result = await response.Content.ReadFromJsonAsync<ErrorResponse>(jsonSerializerOptions, cancellationToken);
                        return ClientResult<TResponse>.SetError(result.Error);
                    }
                case System.Net.HttpStatusCode.Unauthorized:
                    {
                        var result = await response.Content.ReadFromJsonAsync<ErrorResponse>(jsonSerializerOptions, cancellationToken);
                        return ClientResult<TResponse>.SetError(result.Error);
                    }
                case System.Net.HttpStatusCode.MovedPermanently:
                    {
                        var result = await response.Content.ReadAsStringAsync(cancellationToken);
                        return ClientResult<TResponse>.SetError("Invalid Ats name");
                    }
                default:
                    throw new InvalidOperationException("Unknown response status.");
            }
        }

        async Task<ClientResult> ProcessResponseAsync(AtsCommand command, Dictionary<string, string> parameters, CancellationToken cancellationToken)
        {
            var response = await GetResponseAsync(command, parameters, cancellationToken);

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    {
                        return ClientResult.Success();
                    }
                case System.Net.HttpStatusCode.BadRequest:
                    {
                        var result = await response.Content.ReadFromJsonAsync<ErrorResponse>(jsonSerializerOptions, cancellationToken);
                        return ClientResult.SetError(result.Error);
                    }
                default:
                    throw new InvalidOperationException("Unknown response status.");
            }
        }

        FormUrlEncodedContent MakeRequestBody(AtsCommand command, Dictionary<string, string> args = null)
        {
            var defaultParams = new Dictionary<string, string>
                    {
                        { "cmd", command.ToString() },
                        { "token", options.Key }
                    };
            IEnumerable<KeyValuePair<string, string>> parameters;
            if (args != null)
                parameters = defaultParams.Concat(args);
            else
                parameters = defaultParams;

            return new FormUrlEncodedContent(parameters);
        }

        static IEnumerable<CallModel> CreateListOfCalls(string content)
        {
            List<CallModel> result = new();
            var rows = content.Split("\r\n");
            foreach (var line in rows)
            {
                if (line != "")
                {
                    var arr = line.Split(",");

                    result.Add(new CallModel()
                    {
                        CallId = arr[0],
                        Type = Enum.Parse<CallDirection>(arr[1], true),
                        Client = arr[2],
                        Account = arr[3],
                        Via = arr[4],
                        Start = arr[5],
                        Wait = arr[6],
                        Duration = arr[7],
                        Record = !string.IsNullOrEmpty(arr[8]) ? new Uri(arr[8]) : null,
                        QualityControl = arr.Length > 9 ? arr[9] : null,
                    });
                }
            }
            return result;
        }

        static string ToSnakeCase(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (text.Length < 2)
            {
                return text;
            }
            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(text[0]));
            for (int i = 1; i < text.Length; ++i)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        #endregion
    }
}