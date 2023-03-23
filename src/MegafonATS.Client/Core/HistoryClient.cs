using MegafonATS.Client.Core.Abstract;
using MegafonATS.Client.Models.Requests;
using MegafonATS.Client.Models.Responses.History;
using MegafonATS.Client.Results;
using Microsoft.Extensions.Logging;

namespace MegafonATS.Client.Core
{
    public class HistoryClient : ClientBase
    {
        const string apiEndpoint = "/history/json";

        public HistoryClient(HttpClient httpClient, MegafonAtsOptions options, ILogger<ClientBase> logger) : base(httpClient, options, logger) { }

        public async Task<ClientResult<HistoryResponse>> GetHistoryAsync(HistoryRequest request, CancellationToken cancellationToken)
        {
            var result = await ExecuteGetAsync<CallResponse[]>(apiEndpoint, request, cancellationToken);

            if (!result.IsSuccess)
                return ClientResult<HistoryResponse>.SetError(result.Error);

            var data = result.Data;
            if (data == null)
                data = new CallResponse[0];
            return ClientResult<HistoryResponse>.Success(new HistoryResponse { Calls = data });
        }
    }
}