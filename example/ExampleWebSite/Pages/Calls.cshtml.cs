using BrandUp.Website.Pages;
using MegafonATS.Client;
using MegafonATS.Models.Client;

namespace ExampleWebSite.Pages
{
    public class CallsModel : AppPageModel
    {
        public override string Title => "История звонков";

        public IEnumerable<CallModel> Calls { get; set; }

        private IMegafonAtsClient _client;

        public CallsModel(IMegafonAtsClient client)
        {
            _client = client;
        }

        protected override async Task OnPageRequestAsync(PageRequestContext context)
        {
            var clientResult = await _client.HistoryAsync(FilterPeriod.today, FilterCallType.All);
            if (clientResult.IsSuccess == true)
                Calls = clientResult.Result;

            await base.OnPageRequestAsync(context);
        }

        public async Task OnPost()
        {
            var start = Request.Form["StartDate"];
            var end = Request.Form["EndDate"];
            var type = Request.Form["type"];
            var limit = Request.Form["Limit"];
            IClientResult result;
            if (limit != "")
            {
                result = await _client.HistoryAsync(Convert.ToDateTime(start), Convert.ToDateTime(end), Enum.Parse<FilterCallType>(type), Convert.ToInt32(limit));

            }
            else
            {
                result = await _client.HistoryAsync(Convert.ToDateTime(start), Convert.ToDateTime(end), Enum.Parse<FilterCallType>(type));
            }
            if (result.IsSuccess == true)
                Calls = (IEnumerable<CallModel>)result.Result;
        }

        public void OnGet()
        {
        }
    }
}