using MegafonATS.Client.Core.Abstract;
using MegafonATS.Client.Results;
using MegafonATS.Models.Client.Requests;
using MegafonATS.Models.Client.Responses;
using Microsoft.Extensions.Logging;

namespace MegafonATS.Client.Core
{
    public class CallClient : ClientBase
    {
        const string apiEndpoint = "/makecall";
        public CallClient(HttpClient httpClient, MegafonAtsOptions options, ILogger<ClientBase> logger) : base(httpClient, options, logger)
        {
        }

        public async Task<ClientResult<MakeCallResponse>> MakeCallAsync(MakeCallRequest request, CancellationToken cancellationToken) =>
            await ExecuteAsync<MakeCallResponse>(HttpMethod.Post, apiEndpoint, request, cancellationToken);
    }
}
