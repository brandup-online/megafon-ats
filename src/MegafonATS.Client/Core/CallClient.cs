using MegafonATS.Models.Client.Requests;
using MegafonATS.Models.Client.Responses;

namespace MegafonATS.Client.Core
{
    public class CallClient : ClientBase
    {
        const string apiEndpoint = "/makecall";
        public CallClient(HttpClient httpClient, MegafonAtsOptions options) : base(httpClient, options)
        {
        }

        public async Task<MakeCallResponse> MakeCallAsync(MakeCallRequest request, CancellationToken cancellationToken)
        {
            var result = await ExecuteAsync<MakeCallResponse>(HttpMethod.Post, apiEndpoint, request, cancellationToken);
            if (!result.IsSuccess)
                throw new Exception(result.Error);

            return result.Result;
        }

    }
}
