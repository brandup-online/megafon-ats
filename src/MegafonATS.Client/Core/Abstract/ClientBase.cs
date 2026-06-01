using MegafonATS.Client.Exceptions;
using MegafonATS.Client.Models.Requests;
using MegafonATS.Client.Results;
using MegafonATS.Client.Utility;
using MegafonATS.Models.Utility;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MegafonATS.Client.Core.Abstract
{
    public class ClientBase
    {
        readonly string baseUri;

        readonly HttpClient httpClient;
        readonly MegafonAtsOptions options;
        readonly static JsonSerializerOptions jsonSerializerOptions;

        readonly ILogger<ClientBase> logger;

        static ClientBase()
        {
            jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance, PropertyNameCaseInsensitive = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(SnakeCaseNamingPolicy.Instance));
            jsonSerializerOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-ddTHH:mm:ss"));
        }

        public ClientBase(HttpClient httpClient, MegafonAtsOptions options, ILogger<ClientBase> logger)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (string.IsNullOrEmpty(options.Name))
                throw new ArgumentException("АТС Name не может быть пустым.", nameof(options.Name));
            if (string.IsNullOrEmpty(options.Key))
                throw new ArgumentException("АТС Key не может быть пустым.", nameof(options.Key));

            baseUri = $"https://{this.options.Name}.megapbx.ru/crmapi/v1";
        }

        protected async Task<ClientResult<TResponse>> ExecuteGetAsync<TResponse>(string endpoint, CancellationToken cancellationToken) =>
           await ExecuteGetAsync<TResponse>(endpoint, null, cancellationToken);

        protected async Task<ClientResult<TResponse>> ExecuteGetAsync<TResponse>(string endpoint, IRequestModel request, CancellationToken cancellationToken)
        {
            var getRequest = endpoint;
            if (request != null)
            {
                var query = GenerateGetRequestString(request.GetType().GetProperties(), request);
                if (!string.IsNullOrEmpty(query))
                    getRequest += "?" + query;
            }

            return await ExecuteAsync<TResponse>(HttpMethod.Get, getRequest, null, cancellationToken);
        }

        protected async Task<ClientResult<TResponse>> ExecuteAsync<TResponse>(HttpMethod method, string endpoint, IRequestModel request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Выполняется запрос к атс. Метод: {Method}, Адрес: {Endpoint}", method, endpoint);

            using var message = ProcessRequest(method, endpoint, request);
            using var response = await httpClient.SendAsync(message, cancellationToken);

            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                if ((int)response.StatusCode >= 500)
                    throw new MegafonAtsClientException("Внутренняя ошибка сервера.");

                logger.LogWarning("Запрос вернул не успешный статус код.");

                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseString, jsonSerializerOptions);
                return ClientResult<TResponse>.SetError(errorResponse);
            }

            logger.LogInformation("Запрос вернул успешный статус код.");

            if (string.IsNullOrEmpty(responseString) || responseString == "null")
                return ClientResult<TResponse>.Success();

            var deserializeResponse = JsonSerializer.Deserialize<TResponse>(responseString, jsonSerializerOptions);
            return ClientResult<TResponse>.Success(deserializeResponse);
        }

        protected async Task<ClientResult> ExecuteAsync(HttpMethod method, string endpoint, IRequestModel request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Выполняется запрос к атс. Метод: {Method}, Адрес: {Endpoint}", method, endpoint);

            using var message = ProcessRequest(method, endpoint, request);
            using var response = await httpClient.SendAsync(message, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                if ((int)response.StatusCode >= 500)
                    throw new MegafonAtsClientException("Внутренняя ошибка сервера.");

                logger.LogWarning("Запрос вернул не успешный статус код.");

                var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseString, jsonSerializerOptions);
                return ClientResult.SetError(errorResponse);
            }

            logger.LogInformation("Запрос вернул успешный статус код.");
            return ClientResult.Success();
        }

        static string GenerateGetRequestString(PropertyInfo[] properties, IRequestModel request)
        {
            var parts = new List<string>();

            foreach (var property in properties)
            {
                var value = property.GetValue(request);
                if (value == null)
                    continue;

                string stringValue;
                if (property.PropertyType == typeof(DateTime?))
                    stringValue = ((DateTime)value).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
                else
                    stringValue = SnakeCaseNamingPolicy.ToSnakeCase(value.ToString());

                parts.Add($"{SnakeCaseNamingPolicy.ToSnakeCase(property.Name)}={Uri.EscapeDataString(stringValue)}");
            }

            return string.Join("&", parts);
        }

        #region Helpers

        HttpRequestMessage ProcessRequest(HttpMethod method, string endpoint, IRequestModel request)
        {
            var requestUri = new Uri(baseUri + endpoint);
            var message = new HttpRequestMessage(method, requestUri);

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
            var jsonString = JsonSerializer.Serialize(request, request.GetType(), jsonSerializerOptions);
            return new StringContent(jsonString);
        }

        #endregion
    }
}
