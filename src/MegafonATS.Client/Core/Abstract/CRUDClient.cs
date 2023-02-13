using MegafonATS.Client.Models.Requests;
using MegafonATS.Client.Results;
using Microsoft.Extensions.Logging;

namespace MegafonATS.Client.Core.Abstract
{
    public abstract class CRUDClient : ClientBase
    {
        protected CRUDClient(HttpClient httpClient, MegafonAtsOptions options, ILogger<ClientBase> logger) : base(httpClient, options, logger) { }

        protected async Task<ClientResult<TModel>> CreateAsync<TModel>(string apiEndpoint, IRequestModel request, CancellationToken cancellationToken) =>
            await ExecuteAsync<TModel>(HttpMethod.Post, apiEndpoint, request, cancellationToken);

        protected async Task<ClientResult<TModel>> GetAllAsync<TModel>(string apiEndpoint, CancellationToken cancellationToken) =>
            await ExecuteGetAsync<TModel>(apiEndpoint, cancellationToken);

        protected async Task<ClientResult<TModel>> GetAsync<TModel>(string apiEndpoint, CancellationToken cancellationToken) =>
           await ExecuteGetAsync<TModel>(apiEndpoint, cancellationToken);



        protected async Task<ClientResult<TModel>> UpdateAsync<TModel>(string apiEndpoint, IRequestModel request, CancellationToken cancellationToken) =>
            await ExecuteAsync<TModel>(HttpMethod.Put, apiEndpoint, request, cancellationToken);

        protected async Task<ClientResult> DeleteAsync(string apiEndpoint, CancellationToken cancellationToken) =>
           await ExecuteAsync(HttpMethod.Delete, apiEndpoint, null, cancellationToken);

    }
}