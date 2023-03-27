using MegafonATS.Client.Core;
using MegafonATS.Client.Models;
using MegafonATS.Client.Models.Requests;

namespace MegafonATS.Client
{
    public class HistoryClientTest : ClientTestBase
    {
        HistoryClient client;
        protected override Task OnInitializeAsync(IServiceProvider services)
        {
            client = CreateClient<HistoryClient>();
            return base.OnInitializeAsync(services);
        }

        [Fact]
        public async Task GetHistoryAsync_Filter_Success()
        {
            var request = new HistoryRequest
            {
                Period = Models.Period.Today,
                Type = Models.FilterCallType.All,
                Limit = 100
            };

            var result = await client.GetHistoryAsync(request, default);

            Assert.True(result.IsSuccess);
            Assert.True(result.Data.Calls.Length < 100);
            foreach (var call in result.Data.Calls)
            {
                Assert.True(call.Start > DateTime.UtcNow.Date || call.Start > DateTime.UtcNow.Date.AddDays(-1));
            }
        }

        [Fact]

        public async Task GetHistoryAsync_Dates_Success()
        {
            var request = new HistoryRequest
            {
                Start = DateTime.UtcNow.AddDays(-5),
                End = DateTime.UtcNow.AddDays(-3),
                Type = Models.FilterCallType.Out,
                Limit = 10
            };

            var result = await client.GetHistoryAsync(request, default);

            Assert.True(result.IsSuccess);
            foreach (var call in result.Data.Calls)
            {
                Assert.Equal(ClientCallDirection.Out, call.Direction);
            }
        }

        [Fact]
        public async Task GetHistoryAsync_NoLimit_Success()
        {
            var request = new HistoryRequest
            {
                Start = DateTime.UtcNow.AddHours(-3),
                End = DateTime.UtcNow,
            };

            var result = await client.GetHistoryAsync(request, default);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data.Calls);
        }

        [Fact]
        public async Task GetHistoryAsync_Empty_Success()
        {
            var request = new HistoryRequest
            {
            };

            var result = await client.GetHistoryAsync(request, default);

            Assert.True(result.IsSuccess);
        }
    }
}