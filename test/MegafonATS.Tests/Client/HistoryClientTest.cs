using MegafonATS.Client.Core;
using MegafonATS.Models.Client.Requests;

namespace MegafonATS.Client
{
    public class HistoryClientTest : ClientTestBase
    {

        [Fact]
        public async Task GetHistoryAsync_Filter_Success()
        {
            var client = CreateClient<HistoryClient>();

            var request = new HistoryRequest
            {
                Period = Models.Enums.FilterPeriod.Today,
                Type = Models.Enums.FilterCallType.All,
                Limit = 100
            };

            var result = await client.GetHistoryAsync(request, default);

            Assert.NotNull(result);

            Assert.True(result.Data.Calls.Length < 100);
            foreach (var call in result.Data.Calls)
            {
                Assert.True(call.Start > DateTime.UtcNow.Date);
            }
        }

        [Fact]

        public async Task GetHistoryAsync_Dates_Success()
        {
            var client = CreateClient<HistoryClient>();

            var request = new HistoryRequest
            {
                Start = DateTime.UtcNow.AddDays(-5),
                End = DateTime.UtcNow.AddDays(-3),
                Type = Models.Enums.FilterCallType.Out,
                Limit = 10
            };

            var result = await client.GetHistoryAsync(request, default);

            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            foreach (var call in result.Data.Calls)
            {
                Assert.Equal(Models.CallDirection.Out, call.Direction);
            }
        }

        [Fact]
        public async Task GetHistoryAsync_Empty_Success()
        {
            var client = CreateClient<HistoryClient>();

            var request = new HistoryRequest
            {
            };

            var result = await client.GetHistoryAsync(request, default);

            Assert.NotNull(result);
        }
    }
}
