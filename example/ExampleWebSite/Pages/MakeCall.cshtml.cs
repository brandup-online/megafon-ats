using BrandUp.Website.Pages;
using MegafonATS.Client;
using MegafonATS.Models.Client;

namespace ExampleWebSite.Pages
{
    public class MakeCallModel : AppPageModel
    {
        public override string Title => "Сделать звонок";

        private IMegafonAtsClient client;
        public IEnumerable<AccountModel> Users { get; set; }

        public MakeCallModel(IMegafonAtsClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        protected override async Task OnPageRequestAsync(PageRequestContext context)
        {

            var clientResult = await client.AccountsAsync();
            if (clientResult.IsSuccess == true)
                Users = clientResult.Result;

        }


        public void OnPost()
        {
            var user = Users.FirstOrDefault(u => u.RealName == Request.Form["Caller"]);
            var number = Request.Form["PhoneNumber"];

            client.MakeCallAsync(user.Name, number);
        }
    }
}
