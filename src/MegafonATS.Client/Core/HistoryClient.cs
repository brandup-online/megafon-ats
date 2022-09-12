using MegafonATS.Client.Core.Abstract;
using MegafonATS.Client.Results;
using MegafonATS.Models.Client.Requests;
using MegafonATS.Models.Client.Responses.History;
using Microsoft.Extensions.Logging;

namespace MegafonATS.Client.Core
{
    public class HistoryClient : ClientBase
    {
        const string apiEndpoint = "/history/json";

        public HistoryClient(HttpClient httpClient, MegafonAtsOptions options, ILogger<ClientBase> logger) : base(httpClient, options, logger)
        {
        }

        public async Task<ClientResult<HistoryResponse>> GetHistoryAsync(HistoryRequest request, CancellationToken cancellationToken)
        {

            var result = await ExecuteGetAsync<CallResponse[]>(apiEndpoint, request, cancellationToken);

            if (!result.IsSuccess)
            {
                return ClientResult<HistoryResponse>.SetError(result.Error);
            }

            return ClientResult<HistoryResponse>.Success(new HistoryResponse { Calls = result.Data });
        }
    }
}
