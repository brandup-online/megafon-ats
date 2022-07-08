using BrandUp.Website.Pages;
using MefafonATS.Model;
using MefafonATS.Model.ClientModels;

namespace ExampleWebSite.Pages
{
    public class MakeCallModel : AppPageModel
    {
        public override string Title => "Сделать звонок";

        private IMegafonAtsClient _client;
        public IEnumerable<MATSUserModel> Users { get; set; }

        public MakeCallModel(IMegafonAtsClient client)
        {
            _client = client;
        }

        protected override async Task OnPageRequestAsync(PageRequestContext context)
        {

            var clientResult = await _client.AccountsAsync();
            if (clientResult.IsSuccess == true)
                Users = clientResult.Result;

            //return base.OnPageRequestAsync(context);
        }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            var user = Users.FirstOrDefault(u => u.RealName == Request.Form["Caller"]);
            var number = Request.Form["PhoneNumber"];

            _client.MakeCallAsync(user.Name, number);
        }
    }
}
