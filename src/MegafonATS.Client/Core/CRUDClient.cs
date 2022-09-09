using MegafonATS.Models.Client.Requests;

namespace MegafonATS.Client.Core
{
    public abstract class CRUDClient : ClientBase
    {
        protected CRUDClient(HttpClient httpClient, MegafonAtsOptions options) : base(httpClient, options)
        {
        }

        protected async Task<TModel> CreateAsync<TModel>(string apiEndpoint, IRequestModel request, CancellationToken cancellationToken)
        {
            var result = await ExecuteAsync<TModel>(HttpMethod.Post, apiEndpoint, request, cancellationToken);

            if (!result.IsSuccess)
                throw new Exception(result.Error); //Плохо: если падаем то падать надо сразу

            return result.Result;
        }

        protected async Task<TModel> GetAllAsync<TModel>(string apiEndpoint, CancellationToken cancellationToken)
        {
            var result = await ExecuteGetAsync<TModel>(apiEndpoint, cancellationToken);

            if (!result.IsSuccess)
                throw new Exception(result.Error); //Плохо: если падаем то падать надо сразу

            return result.Result;
        }
        protected async Task<TModel> GetAsync<TModel>(string apiEndpoint, CancellationToken cancellationToken)
        {
            var result = await ExecuteGetAsync<TModel>(apiEndpoint, cancellationToken);

            if (!result.IsSuccess)
                throw new Exception(result.Error); //Плохо: если падаем то падать надо сразу

            return result.Result;
        }
        protected async Task<TModel> UpdateAsync<TModel>(string apiEndpoint, IRequestModel request, CancellationToken cancellationToken)
        {
            var result = await ExecuteAsync<TModel>(HttpMethod.Put, apiEndpoint, request, cancellationToken);

            if (!result.IsSuccess)
                throw new Exception(result.Error); //Плохо: если падаем то падать надо сразу

            return result.Result;
        }
        protected async Task DeleteAsync(string apiEndpoint, CancellationToken cancellationToken)
        {
            var result = await ExecuteAsync(HttpMethod.Delete, apiEndpoint, null, cancellationToken);

            if (!result.IsSuccess)
                throw new Exception(result.Error); //Плохо: если падаем то падать надо сразу
        }
    }
}
