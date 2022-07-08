using MefafonATS.Model.ClientModels;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MefafonATS.Model
{
    public class MegafonAtsClient : IMegafonAtsClient
    {
        readonly HttpClient _httpClient;
        readonly MegafonAtsOptions options;
        readonly static JsonSerializerOptions jsonSerializerOptions;

        static MegafonAtsClient()
        {
            jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

            };
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public MegafonAtsClient(HttpClient httpClient, MegafonAtsOptions options)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.options = options ?? throw new ArgumentNullException(nameof(options));

            _httpClient.BaseAddress = new Uri($"https://{this.options.AtsName}.megapbx.ru/sys/crm_api.wcgp");
        }

        #region IMegafonAtsClient member

        /// <summary>
        /// Запрос от CRM к Виртуальной АТС для получения сотрудников.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Коллекцию отделов</returns>
        public async Task<ClientResult<IEnumerable<MATSUserModel>>> AccountsAsync(CancellationToken cancellationToken = default) =>
            await ProcessResponseAsync<IEnumerable<MATSUserModel>>(ECommand.accounts, new Dictionary<string, string>() { }, cancellationToken);


        /// <summary>
        /// Запрос от CRM к Виртуальной АТС для получения отделов для конкретного сотрудника
        /// </summary>
        /// <param name="user">идентификатор сотрудника Виртуальной АТС</param>
        /// <param name="cancellationToken">Коллекцию отделов</param>
        /// <returns>Коллекцию отделов</returns>
        public async Task<ClientResult<IEnumerable<MATSGroupModel>>> GroupsAsync(string user, CancellationToken cancellationToken = default) =>
            await ProcessResponseAsync<IEnumerable<MATSGroupModel>>(ECommand.groups, new Dictionary<string, string>() { { "user", user } }, cancellationToken);

        /// <summary>
        /// Запрос от CRM к Виртуальной АТС для получения отделов
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Коллекцию отделов</returns>
        public async Task<ClientResult<IEnumerable<MATSGroupModel>>> GroupsAsync(CancellationToken cancellationToken = default) =>
            await ProcessResponseAsync<IEnumerable<MATSGroupModel>>(ECommand.groups, new Dictionary<string, string>(), cancellationToken);

        /// <summary>
        /// Команда необходима для того, чтобы получить из Виртуальной АТС историю звонков за нужный период времени.
        /// </summary>
        /// <param name="period">период времени за который нужно выгрузить данные.</param>
        /// <param name="type">тип звонка</param>
        /// <param name="limit">результат должен содержать не более, чем limit записей </param>
        /// <param name="cancellationToken"></param>
        /// <returns>Коллекцию объектов с информацией о звонках</returns>
        public async Task<ClientResult<IEnumerable<MATSCallModel>>> HistoryAsync(EPeriod period, ECallType type, int limit = int.MaxValue, CancellationToken cancellationToken = default) =>
             await ProcessResponseAsync<IEnumerable<MATSCallModel>>(ECommand.history,
                 new Dictionary<string, string>()
            {
                { "period", period.ToString().ToLower() },
                { "type", type.ToString().ToLower() },
                { "limit", limit.ToString() }
            }, cancellationToken);

        /// <summary>
        /// Команда необходима для того, чтобы получить из Виртуальной АТС историю звонков за нужный период времени.
        /// </summary>
        /// <param name="start">начало периода для выгрузки данных</param>
        /// <param name="end">окончание периода для выгрузки данных</param>
        /// <param name="type">тип звонка</param>
        /// <param name="limit">результат должен содержать не более, чем limit записей</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Коллекцию объектов с информацией о звонках</returns>
        public async Task<ClientResult<IEnumerable<MATSCallModel>>> HistoryAsync(DateTime start, DateTime end, ECallType type, int limit = int.MaxValue, CancellationToken cancellationToken = default) =>
            await ProcessResponseAsync<IEnumerable<MATSCallModel>>(ECommand.history,
                new Dictionary<string, string>()
            {
                { "start", start.ToString("yyyymmddThhmmssZ") },
                { "end", end.ToString("yyyymmddThhmmssZ") },
                { "type", type.ToString().ToLower() },
                { "limit", limit.ToString() }
            }, cancellationToken);

        /// <summary>
        /// Команда необходимая для того, чтобы инициировать звонок от менеджера клиенту. В результате
        /// успешного выполнения команды, Виртуальная АТС сделает сначала звонок на телефон менеджера,
        /// а потом соединит его с клиентом.Команда может использоваться, например, для звонка по клику
        /// на номер клиента в вашей CRM или базе данных.
        /// </summary>
        /// <param name="user">идентификатор сотрудника Виртуальной АТС</param>
        /// <param name="phoneNumber">"номер телефона клиента"</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Уникальный идентификатор звонка</returns>
        public async Task<ClientResult<MATSCurrentCallModel>> MakeCallAsync(string user, string phoneNumber, CancellationToken cancellationToken = default) =>
            await ProcessResponseAsync<MATSCurrentCallModel>(ECommand.makeCall, new Dictionary<string, string>() { { "user", user }, { "phone", phoneNumber } }, cancellationToken);

        /// <summary>
        /// Запрос от CRM к Виртуальной АТС для включения / выключения приема звонков сотрудником во всех
        /// его отделах или конкретном отделе.
        /// </summary>
        /// <param name="user">идентификатор сотрудника Виртуальной АТС</param>
        /// <param name="group_id">идентификатор отдела АТС, в которомнадо выключить/включить прием звонков </param>
        /// <param name="status">on - чтобы включить прием звонков, off - чтобы выключить прием звонков</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Информация о успехе/неуспехе операции</returns>
        public async Task<ClientResult> SubscribeOnCallsAsync(string user, string group_id, ESubscriptionStatus status, CancellationToken cancellationToken = default)
        {
            return await ProcessResponseAsync(ECommand.subscribeOnCalls, new Dictionary<string, string>() { { "user", user }, { "group_id", group_id }, { "status", status.ToString().ToLower() } }, cancellationToken);
        }

        /// <summary>
        /// Запрос от CRM к Виртуальной АТС для проверки факта приема звонков сотрудником в конкретном отделе
        /// </summary>
        /// <param name="user">идентификатор сотрудника Виртуальной АТС</param>
        /// <param name="group_id">идентификатор отдела АТС, для которого надо выполнить проверку</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ClientResult<StatusModel>> SubscriptionStatusAsync(string user, string group_id, CancellationToken cancellationToken = default) =>
             await ProcessResponseAsync<StatusModel>(ECommand.subscriptionStatus, new Dictionary<string, string>() { { "user", user }, { "group_id", group_id } }, cancellationToken);

        /// <summary>
        /// Запрос от CRM к Виртуальной АТС позволяет включить или выключить прием звонков сотрудником Виртуальной АТС.
        /// </summary>
        /// <param name="user">идентификатор сотрудника Виртуальной АТС </param>
        /// <param name="state">true - выключен,  false - включен </param>
        /// <param name="cancellationToken"></param>
        /// <returns>Информация об успехе/неуспехе операции</returns>
        public async Task<ClientResult> SetDnDAsync(string user, bool state, CancellationToken cancellationToken = default) =>
            await ProcessResponseAsync(ECommand.set_dnd, new Dictionary<string, string>() { { "user", user }, { "state", state.ToString().ToLower() } }, cancellationToken);


        /// <summary>
        /// Запрос от CRM к Виртуальной АТС позволяет узнать включен или выключен прием звонков сотрудником Виртуальной АТС.
        /// </summary>
        /// <param name="user">идентификатор сотрудника Виртуальной АТС</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ClientResult<MATSDnDModel>> GetDnDAsync(string user, CancellationToken cancellationToken = default)
        {
            return await ProcessResponseAsync<MATSDnDModel>(ECommand.get_dnd, new Dictionary<string, string>() { { "user", user } }, cancellationToken);
        }

        #endregion

        #region Helpers

        enum ECommand
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
        async Task<HttpResponseMessage> GetResponseAsync(string methodName, Dictionary<string, string> parameters, CancellationToken cancellationToken)
        {
            var values = MakeRequestBody(methodName, parameters);
            var tmp = await values.ReadAsStringAsync();
            return await _httpClient.PostAsync(_httpClient.BaseAddress, values, cancellationToken);
        }

        //async Task<ClientResult<IEnumerable<MATSCallModel>>> ProcessResponseHistoryAsync(Dictionary<string, string> parameters, CancellationToken cancellationToken)
        //{
        //    if (parameters["limit"] == "0")
        //    {
        //        parameters.Remove("limit");
        //    }
        //    var response = await GetResponseAsync("history", parameters, cancellationToken);
        //    switch (response.StatusCode)
        //    {
        //        case System.Net.HttpStatusCode.OK:
        //            {
        //                var str = await response.Content.ReadAsStringAsync();
        //                return ClientResult<IEnumerable<MATSCallModel>>.Success(MATSCallModel.CreateListOfCalls(str));
        //            }
        //        case System.Net.HttpStatusCode.BadRequest:
        //            {
        //                var result = await response.Content.ReadFromJsonAsync<ErrorResponse>(jsonSerializerOptions, cancellationToken);
        //                return ClientResult<IEnumerable<MATSCallModel>>.SetError(result.Errror);
        //            }
        //        default:
        //            throw new InvalidOperationException("Unknown response status.");
        //    }

        //}



        async Task<ClientResult<TResponse>> ProcessResponseAsync<TResponse>(ECommand command, Dictionary<string, string> parameters, CancellationToken cancellationToken) where TResponse : class
        {
            var methodName = command.ToString();
            if (methodName == null)
                throw new ArgumentNullException(nameof(methodName));

            var response = await GetResponseAsync(methodName, parameters, cancellationToken);

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    {
                        if (typeof(TResponse) == typeof(IEnumerable<MATSCallModel>))
                        {
                            var str = await response.Content.ReadAsStringAsync();
                            var result = ClientResult<IEnumerable<MATSCallModel>>.Success(CreateListOfCalls(str));


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
                default:
                    throw new InvalidOperationException("Unknown response status.");
            }
        }

        async Task<ClientResult> ProcessResponseAsync(ECommand command, Dictionary<string, string> parameters, CancellationToken cancellationToken)
        {
            var methodName = command.ToString();

            if (methodName == null)
                throw new ArgumentNullException(nameof(methodName));

            var response = await GetResponseAsync(methodName, parameters, cancellationToken);

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    {
                        return ClientResult.Success();
                    }
                case System.Net.HttpStatusCode.BadRequest:
                    {
                        var result2 = await response.Content.ReadAsStringAsync();
                        var result = await response.Content.ReadFromJsonAsync<ErrorResponse>(jsonSerializerOptions, cancellationToken);
                        return ClientResult.SetError(result.Error);
                    }
                default:
                    throw new InvalidOperationException("Unknown response status.");
            }
        }



        FormUrlEncodedContent MakeRequestBody(string cmd, Dictionary<string, string> args = null)
        {
            var defaultParams = new Dictionary<string, string>
                    {
                        { "cmd", cmd },
                        { "token", options.Token }
                    };
            IEnumerable<KeyValuePair<string, string>> parameters;
            if (args != null)
            {
                parameters = defaultParams.Concat(args);
            }
            else parameters = defaultParams;
            var content = new FormUrlEncodedContent(parameters);
            return content;
        }

        IEnumerable<MATSCallModel> CreateListOfCalls(string content)
        {
            List<MATSCallModel> result = new();
            var rows = content.Split("\n");
            foreach (var line in rows)
            {
                if (line != "")
                {
                    var arr = line.Split(",");

                    result.Add(new MATSCallModel()
                    {
                        UID = arr[0],
                        Type = arr[1],
                        Client = arr[2],
                        Account = arr[3],
                        Via = arr[4],
                        Start = arr[5],
                        Wait = arr[6],
                        Duration = arr[7],
                        Record = arr[8],
                    });
                }
            }
            return result;
        }

        #endregion
    }

}