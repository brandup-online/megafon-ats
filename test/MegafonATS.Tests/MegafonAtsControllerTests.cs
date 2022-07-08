using MefafonATS.Webhooks;

namespace MefafonATS.Model.Tests
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
                        { "start", "20220101T010101" },
                        { "duration", "50" },
                        { "callid", "43294397431" },
                        { "link", "jjj.net" },
                        { "rating", "4" },
                        { "crm_token", secretKey },
                        { "status", "Success" }


                    };

            var content = new FormUrlEncodedContent(values);

            var result = await client.PostAsync("megafon/callback", content);

            var ev = (MyTestService)factory.Services.GetService(typeof(IMegafonAtsEvents));

            Assert.True(result.IsSuccessStatusCode);
            Assert.NotNull(ev);
            Assert.Equal(MyTestService.History.Type, values["type"]);
            Assert.Equal(MyTestService.History.User, values["user"]);
            Assert.Equal(MyTestService.History.Ext, values["ext"]);
            Assert.Equal(MyTestService.History.groupRealName, values["groupRealName"]);
            Assert.Equal(MyTestService.History.Telnum, values["telnum"]);
            Assert.Equal(MyTestService.History.Phone, values["phone"]);
            Assert.Equal(MyTestService.History.Diversion, values["diversion"]);
            Assert.Equal(MyTestService.History.Start.ToString("yyyyMMddThhmmss"), values["start"]);
            Assert.Equal(MyTestService.History.Duration.ToString(), values["duration"]);
            Assert.Equal(MyTestService.History.Callid, values["callid"]);
            Assert.Equal(MyTestService.History.Link, values["link"]);
            Assert.Equal(MyTestService.History.Rating.ToString(), values["rating"]);
            Assert.Equal(MyTestService.History.Status, values["status"]);
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
                        { "direction", "9in99999" },
                        { "callid", "43294397431" },
                        { "status", "Success" },
                        { "crm_token", secretKey }
                    };

            var content = new FormUrlEncodedContent(values);

            var result = await client.PostAsync("megafon/callback", content);

            var ev = (MyTestService)factory.Services.GetService(typeof(IMegafonAtsEvents));

            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal(MyTestService.Event.Type, values["type"]);
            Assert.Equal(MyTestService.Event.Phone, values["phone"]);
            Assert.Equal(MyTestService.Event.Diversion, values["diversion"]);
            Assert.Equal(MyTestService.Event.User, values["user"]);
            Assert.Equal(MyTestService.Event.GroupRealName, values["groupRealName"]);
            Assert.Equal(MyTestService.Event.Ext, values["ext"]);
            Assert.Equal(MyTestService.Event.Telnum, values["telnum"]);
            Assert.Equal(MyTestService.Event.Direction, values["direction"]);
            Assert.Equal(MyTestService.Event.Callid, values["callid"]);

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

            var content = new FormUrlEncodedContent(values);

            var result = await client.PostAsync("megafon/callback", content);

            var ev = (MyTestService)factory.Services.GetService(typeof(IMegafonAtsEvents));

            Assert.NotNull(ev);
            Assert.Equal(MyTestService.Contact.Phone, values["phone"]);
            Assert.Equal(MyTestService.Contact.Callid, values["callid"]); ;
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

            var ev = (MyTestService)factory.Services.GetService(typeof(IMegafonAtsEvents));

            Assert.True(result.IsSuccessStatusCode == true);
            Assert.NotNull(ev);
            Assert.Equal(MyTestService.Rating.Callid, values["callid"]);
            Assert.Equal(MyTestService.Rating.Rating, values["rating"]);
            Assert.Equal(MyTestService.Rating.User, values["user"]);
            Assert.Equal(MyTestService.Rating.Phone, values["phone"]);
            Assert.Equal(MyTestService.Rating.Ext, values["ext"]);
        }

    }

    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
            };
    }
}
