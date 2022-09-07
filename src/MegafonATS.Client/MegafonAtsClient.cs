using MegafonATS.Models;
using MegafonATS.Models.Client;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MegafonATS.Client
{
    public class MegafonAtsClient : IMegafonAtsClient
    {
        readonly HttpClient httpClient;
        readonly MegafonAtsOptions options;
        readonly static JsonSerializerOptions jsonSerializerOptions;

        static MegafonAtsClient()
        {
            jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public MegafonAtsClient(HttpClient httpClient, MegafonAtsOptions options)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.options = options ?? throw new ArgumentNullException(nameof(options));

            if (options.Name == null)
                throw new ArgumentNullException(nameof(options.Name));
            if (options.Token == null)
                throw new ArgumentNullException(nameof(options.Token));

            this.httpClient.BaseAddress = new Uri($"https://{this.options.Name}.megapbx.ru/sys/crm_api.wcgp");
        }

        #region IMegafonAtsClient member

        public async Task<ClientResult<IEnumerable<AccountModel>>> AccountsAsync(CancellationToken cancellationToken = default) =>
            await ProcessResponseAsync<IEnumerable<AccountModel>>(AtsCommand.accounts, new Dictionary<string, string>(), cancellationToken);

        public async Task<ClientResult<IEnumerable<GroupModel>>> GroupsAsync(string user, CancellationToken cancellationToken = default) =>
            await ProcessResponseAsync<IEnumerable<GroupModel>>(AtsCommand.groups, new Dictionary<string, string>() { { "user", user } }, cancellationToken);

        public async Task<ClientResult<IEnumerable<GroupModel>>> GroupsAsync(CancellationToken cancellationToken = default) =>
            await ProcessResponseAsync<IEnumerable<GroupModel>>(AtsCommand.groups, new Dictionary<string, string>(), cancellationToken);

        public async Task<ClientResult<IEnumerable<CallModel>>> HistoryAsync(FilterPeriod period, FilterCallType type, int limit = int.MaxValue, CancellationToken cancellationToken = default) =>
             await ProcessResponseAsync<IEnumerable<CallModel>>(AtsCommand.history,
                 new Dictionary<string, string>()
            {
                { "period",ToSnakeCase(period.ToString()) },
                { "type", type.ToString().ToLower() },
                { "limit", limit.ToString() }
            }, cancellationToken);

        public async Task<ClientResult<IEnumerable<CallModel>>> HistoryAsync(DateTime start, DateTime end, FilterCallType type, int limit = int.MaxValue, CancellationToken cancellationToken = default) =>
            await ProcessResponseAsync<IEnumerable<CallModel>>(AtsCommand.history,
                new Dictionary<string, string>()
            {
                { "start", start.ToString("yyyymmddThhmmssZ") },
                { "end", end.ToString("yyyymmddThhmmssZ") },
                { "type", type.ToString().ToLower() },
                { "limit", limit.ToString() }
            }, cancellationToken);

        public async Task<ClientResult<CurrentCallModel>> MakeCallAsync(string user, string phoneNumber, CancellationToken cancellationToken = default) =>
            await ProcessResponseAsync<CurrentCallModel>(AtsCommand.makeCall, new Dictionary<string, string>() { { "user", user }, { "phone", phoneNumber } }, cancellationToken);

        public async Task<ClientResult> SubscribeOnCallsAsync(string user, string groupId, SubscriptionStatus status, CancellationToken cancellationToken = default) =>
            await ProcessResponseAsync(AtsCommand.subscribeOnCalls,
                new Dictionary<string, string>()
            {
                { "user", user },
                { "group_id", groupId },
                { "status", status.ToString().ToLower() }
            }, cancellationToken);

        public async Task<ClientResult> SubscribeOnCallsAsync(string user, SubscriptionStatus status, CancellationToken cancellationToken = default) =>
            await ProcessResponseAsync(AtsCommand.subscribeOnCalls,
                 new Dictionary<string, string>()
            {
                { "user", user },
                { "status", status.ToString().ToLower() }
            }, cancellationToken);


        public async Task<ClientResult<StatusModel>> SubscriptionStatusAsync(string user, string groupId, CancellationToken cancellationToken = default) =>
             await ProcessResponseAsync<StatusModel>(AtsCommand.subscriptionStatus, new Dictionary<string, string>() { { "user", user }, { "group_id", groupId } }, cancellationToken);

        public async Task<ClientResult> SetDnDAsync(string user, bool state, CancellationToken cancellationToken = default) =>
            await ProcessResponseAsync(AtsCommand.set_dnd, new Dictionary<string, string>() { { "user", user }, { "state", state.ToString().ToLower() } }, cancellationToken);

        public async Task<ClientResult<DnDModel>> GetDnDAsync(string user, CancellationToken cancellationToken = default)
        {
            return await ProcessResponseAsync<DnDModel>(AtsCommand.get_dnd, new Dictionary<string, string>() { { "user", user } }, cancellationToken);
        }

        #endregion

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
                        { "token", options.Token }
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