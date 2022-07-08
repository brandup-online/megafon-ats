using BrandUp.Website.Pages;
using MefafonATS.Model.ClientModels;
using MefafonATS.Model.Services;

namespace ExampleWebSite.Pages
{
    public class UsersModel : AppPageModel
    {
        public override string Title => "Пользователи";

        public IEnumerable<MATSUserModel> Users { get; set; }

        private IMegafonClientFactoryService factory;

        public UsersModel(IMegafonClientFactoryService factory)
        {
            this.factory = factory;
        }

        protected override async Task OnPageRequestAsync(PageRequestContext context)
        {
            var client = await factory.CreateAsync(new MefafonATS.Model.MegafonAtsOptions { AtsName = "vats370633", Token = "89244060-639b-45fa-b8cc-6fdf01fab820" });
            var clientResult = await client.AccountsAsync();
            if (clientResult.IsSuccess == true)
                Users = clientResult.Result;



            //return base.OnPageRequestAsync(context);
        }

        public void OnGet()
        {
        }
    }
}
