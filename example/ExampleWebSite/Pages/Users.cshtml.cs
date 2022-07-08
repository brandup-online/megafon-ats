using BrandUp.Website.Pages;
using MegafonATS.Client;
using MegafonATS.Models.Client;

namespace ExampleWebSite.Pages
{
    public class UsersModel : AppPageModel
    {
        public override string Title => "Пользователи";

        public IEnumerable<UserModel> Users { get; set; }

        private IMegafonAtsClientFactory factory;

        public UsersModel(IMegafonAtsClientFactory factory)
        {
            this.factory = factory;
        }

        protected override async Task OnPageRequestAsync(PageRequestContext context)
        {
            var client = factory.Create(new MegafonAtsOptions { Name = "vats370633", Token = "89244060-639b-45fa-b8cc-6fdf01fab820" });
            var clientResult = await client.AccountsAsync();
            if (clientResult.IsSuccess == true)
                Users = clientResult.Result;

            await OnPageRequestAsync(context);
        }

        public void OnGet()
        {
        }
    }
}