using BrandUp.Website.Pages;
using MegafonATS.Client;
using MegafonATS.Models.Client;
using Microsoft.Extensions.Options;

namespace ExampleWebSite.Pages
{
    public class UsersModel : AppPageModel
    {
        public override string Title => "Пользователи";

        public IEnumerable<AccountModel> Users { get; set; }

        IMegafonAtsClientFactory factory;
        IOptions<MegafonAtsOptions> options;

        public UsersModel(IMegafonAtsClientFactory factory, IOptions<MegafonAtsOptions> options
            )
        {
            this.factory = factory;
            this.options = options;
        }

        protected override async Task OnPageRequestAsync(PageRequestContext context)
        {
            IMegafonAtsClient client = factory.Create(new MegafonAtsOptions { Name = options.Value.Name, Token = options.Value.Token });
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