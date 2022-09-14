using MegafonATS.Fakes;
using MegafonATS.Models.Webhooks.Enums;
using MegafonATS.Models.Webhooks.RequestModels;
using MegafonATS.Models.Webhooks.ResponseModels;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace MegafonATS
{
    public class MegafonAtsControllerTests
    {
        private readonly AgentFactory factory;
        private readonly string secretKey = "token";

        public MegafonAtsControllerTests()
        {
            factory = new AgentFactory();
            factory.ClientOptions.BaseAddress = new Uri("https://localhost/");
        }

        [Fact]
        public async Task TestController_HistoryCommand()
        {
            var client = factory.CreateClient();
            var date = DateTime.UtcNow.ToString("yyyyMMddThhmmssZ");
            var values = new Dictionary<string, string>
                    {
                        { "cmd", "history" },
                        { "type", "in" },
                        { "user", "user" },
                        { "ext", "510" },
                        { "groupRealName", "groupname" },
                        { "telnum", "999999" },
                        { "phone", "88005553535" },
                        { "diversion", "100500" },
                        { "start", date },
                        { "duration", "50" },
                        { "callid", "43294397431" },
                        { "link", "https://test.ru/test.mp3" },
                        { "rating", "4" },
                        { "crm_token", secretKey },
                        { "status", WebhookCallStatus.Success.ToString() }
                    };

            var content = new FormUrlEncodedContent(values);

            var result = await client.PostAsync("megafon/callback", content);
            var response = await result.Content.ReadAsStringAsync();
            Assert.True(result.IsSuccessStatusCode);

            var results = factory.Services.GetRequiredService<FakeMegafonAtsEventsResults>();
            Enum.TryParse(values["status"], out WebhookCallStatus status);

            Assert.Equal(results.History.Type, Enum.Parse<WebhookCallDirection>(values["type"], true));
            Assert.Equal(results.History.User, values["user"]);
            Assert.Equal(results.History.UserExt, values["ext"]);
            Assert.Equal(results.History.GroupRealName, values["groupRealName"]);
            Assert.Equal(results.History.UserPhone, values["telnum"]);
            Assert.Equal(results.History.Phone, values["phone"]);
            Assert.Equal(results.History.Diversion, values["diversion"]);
            Assert.Equal(results.History.Start.ToUniversalTime().ToString("yyyyMMddThhmmssZ"), values["start"]);
            Assert.Equal(results.History.Duration.ToString(), values["duration"]);
            Assert.Equal(results.History.CallId, values["callid"]);
            Assert.Equal(results.History.Link, new Uri(values["link"]));
            Assert.Equal(results.History.Rating.ToString(), values["rating"]);
            Assert.Equal(results.History.Status, status);
        }

        [Fact]
        public async Task TestController_EventCommand()
        {
            var client = factory.CreateClient();
            var values = new Dictionary<string, string>
                    {
                        { "cmd", "event" },
                        { "type", "INCOMING" },
                        { "phone", "88005553535" },
                        { "diversion", "100500" },
                        { "user", "user" },
                        { "groupRealName", "groupname" },
                        { "ext", "510" },
                        { "telnum", "999999" },
                        { "direction", "in" },
                        { "callid", "43294397431" },
                        { "status", "Success" },
                        { "crm_token", secretKey }
                    };
            var content = new FormUrlEncodedContent(values);

            var result = await client.PostAsync("megafon/callback", content);
            Assert.True(result.IsSuccessStatusCode);

            var results = factory.Services.GetRequiredService<FakeMegafonAtsEventsResults>();

            Assert.Equal(results.Event.Type, Enum.Parse<EventType>(values["type"], true));
            Assert.Equal(results.Event.Phone, values["phone"]);
            Assert.Equal(results.Event.Diversion, values["diversion"]);
            Assert.Equal(results.Event.User, values["user"]);
            Assert.Equal(results.Event.GroupRealName, values["groupRealName"]);
            Assert.Equal(results.Event.UserExt, values["ext"]);
            Assert.Equal(results.Event.UserPhone, values["telnum"]);
            Assert.Equal(results.Event.Direction, Enum.Parse<WebhookCallDirection>(values["direction"], true));
            Assert.Equal(results.Event.CallId, values["callid"]);
        }

        [Fact]
        public async Task TestController_ContactCommand()
        {
            var client = factory.CreateClient();
            var values = new Dictionary<string, string>
                    {
                        { "cmd", "contact" },
                        { "phone", "88005553535" },
                        { "callid", "43294397431" },
                        { "crm_token", secretKey }
                    };

            var result = await client.PostAsync("megafon/callback", new FormUrlEncodedContent(values));
            Assert.True(result.IsSuccessStatusCode == true);


            var response = await result.Content.ReadFromJsonAsync<ContactResponse>();
            Assert.Equal("Name", response.ContactName);
            Assert.Equal("Manager", response.Responsible);

            var results = factory.Services.GetRequiredService<FakeMegafonAtsEventsResults>();
            Assert.NotNull(results.Contact);
            Assert.Equal(results.Contact.Phone, values["phone"]);
            Assert.Equal(results.Contact.CallId, values["callid"]); ;
        }

        [Fact]
        public async Task TestController_RatingCommand()
        {
            var client = factory.CreateClient();
            var values = new Dictionary<string, string>
                    {
                        { "cmd", "rating" },
                        { "callid", "43294397431" },
                        { "rating", "4" },
                        { "phone", "88005553535" },
                        { "user", "user" },
                        { "ext", "510" },
                        { "crm_token", secretKey }
                    };

            var content = new FormUrlEncodedContent(values);

            var result = await client.PostAsync("megafon/callback", content);
            Assert.True(result.IsSuccessStatusCode == true);

            var results = factory.Services.GetRequiredService<FakeMegafonAtsEventsResults>();

            Assert.Equal(results.Rating.CallId, values["callid"]);
            Assert.Equal(results.Rating.Rating, values["rating"]);
            Assert.Equal(results.Rating.User, values["user"]);
            Assert.Equal(results.Rating.Phone, values["phone"]);
            Assert.Equal(results.Rating.UserExt, values["ext"]);
        }
    }
}