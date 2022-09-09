using MegafonATS.Client.Results;
using MegafonATS.Client.Utility;
using MegafonATS.Models.Client.Requests;
using MegafonATS.Models.Utility;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MegafonATS.Client
{
    public class ClientBase
    {
        readonly string baseUri;

        readonly HttpClient httpClient;
        readonly MegafonAtsOptions options;
        readonly static JsonSerializerOptions jsonSerializerOptions;

        static ClientBase()
        {
            jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(SnakeCaseNamingPolicy.Instance));
            jsonSerializerOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-ddThh:mm:ssZ"));

        }

        public ClientBase(HttpClient httpClient, MegafonAtsOptions options)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.options = options ?? throw new ArgumentNullException(nameof(options));

            if (options.Name == null)
                throw new ArgumentNullException(nameof(options.Name));
            if (options.Key == null)
                throw new ArgumentNullException(nameof(options.Key));

            baseUri = $"https://{this.options.Name}.megapbx.ru/crmapi/v1";
        }

        protected async Task<ClientResult<TResponse>> ExecuteGetAsync<TResponse>(string endpoint, CancellationToken cancellationToken) =>
           await ExecuteGetAsync<TResponse>(endpoint, null, cancellationToken);

        protected async Task<ClientResult<TResponse>> ExecuteGetAsync<TResponse>(string endpoint, IRequestModel request, CancellationToken cancellationToken)
        {
            var getRequest = endpoint;
            if (request != null)
            {
                var propertiesList = request.GetType().GetProperties();
                getRequest += "?" + GenerateGetRequestString(propertiesList, request);
            }

            return await ExecuteAsync<TResponse>(HttpMethod.Get, getRequest, null, cancellationToken);
        }

        protected async Task<ClientResult<TResponse>> ExecuteAsync<TResponse>(HttpMethod method, string endpoint, IRequestModel request, CancellationToken cancellationToken)
        {
            using var message = await ProcessRequestAsync(method, endpoint, request, cancellationToken);
            using var response = await httpClient.SendAsync(message, cancellationToken);

            var responseString = await response.Content.ReadAsStringAsync();
            var jsonString = responseString.Replace("null", "");

            if (!response.IsSuccessStatusCode)
                return ClientResult<TResponse>.SetError(response.StatusCode.ToString());

            TResponse deserializeResponse = JsonSerializer.Deserialize<TResponse>(jsonString, jsonSerializerOptions);
            return ClientResult<TResponse>.Success(deserializeResponse);
        }

        protected async Task<ClientResult> ExecuteAsync(HttpMethod method, string endpoint, IRequestModel request, CancellationToken cancellationToken)
        {
            using var message = await ProcessRequestAsync(method, endpoint, request, cancellationToken);
            using var response = await httpClient.SendAsync(message, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return ClientResult.SetError(response.StatusCode.ToString());

            return ClientResult.Success();
        }

        static string GenerateGetRequestString(PropertyInfo[] properties, IRequestModel request)
        {
            List<string> parts = new();

            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(request) != null)
                    if (property.PropertyType == typeof(DateTime?))
                    {
                        var dateTime = (DateTime)property.GetValue(request);
                        parts.Add(string.Format("{0}={1}", ToSnakeCase(property.Name), dateTime.ToString("yyyy-MM-ddThh:mm:ssZ")));
                    }
                    else
                    {
                        parts.Add(string.Format("{0}={1}", ToSnakeCase(property.Name), ToSnakeCase(property.GetValue(request).ToString())));
                    }
            }

            return string.Join("&", parts);
        }

        #region Helpers

        async Task<HttpRequestMessage> ProcessRequestAsync(HttpMethod method, string endpoint, IRequestModel request, CancellationToken cancellationToken)
        {
            var requestUri = new Uri(baseUri + endpoint);
            using var message = new HttpRequestMessage(method, requestUri);

            if (request != null)
            {
                var content = Serialize(request);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                message.Content = content;
            }

            message.Headers.Add("X-API-KEY", options.Key);
            return message;
        }

        static HttpContent Serialize(IRequestModel request)
        {
            using var stream = new MemoryStream();
            var jsonString = JsonSerializer.Serialize(request, request.GetType(), jsonSerializerOptions);
            return new StringContent(jsonString);
        }



        protected static string ToSnakeCase(string text)
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