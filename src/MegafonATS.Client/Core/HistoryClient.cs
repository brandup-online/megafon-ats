using MegafonATS.Models.Client;
using MegafonATS.Models.Client.Requests;
using MegafonATS.Models.Client.Responses;

namespace MegafonATS.Client.Core
{
    public class HistoryClient : ClientBase
    {
        const string apiEndpoint = "/history/json";
        public HistoryClient(HttpClient httpClient, MegafonAtsOptions options) : base(httpClient, options)
        {
        }

        public async Task<HistoryResponse> GetHistoryAsync(HistoryRequest request, CancellationToken cancellationToken)
        {

            var result = await ExecuteGetAsync<Call[]>(apiEndpoint, request, cancellationToken);

            if (!result.IsSuccess)
                throw new Exception(result.Error);

            return new HistoryResponse { Calls = result.Result };
        }


    }
}
