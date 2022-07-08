using BrandUp.Website.Pages;
using MegafonATS.Client;
using MegafonATS.Models.Client;

namespace ExampleWebSite.Pages
{
    public class GroupsModel : AppPageModel
    {
        public override string Title => "Группы";
        public IEnumerable<GroupModel> Groups { get; set; }
        public IEnumerable<UserModel> Users { get; set; }

        private IMegafonAtsClient _client;

        public GroupsModel(IMegafonAtsClient client)
        {
            _client = client;
        }

        protected override async Task OnPageRequestAsync(PageRequestContext context)
        {

            var clientResult = await _client.GroupsAsync();
            if (clientResult.IsSuccess == true)
                Groups = clientResult.Result;
            var UserResult = await _client.AccountsAsync();
            if (clientResult.IsSuccess == true)
                Users = UserResult.Result;
        }
        public async Task OnPost()
        {
            var user = Users.FirstOrDefault(u => u.RealName == Request.Form["Caller"]);

            var clientResult = await _client.GroupsAsync(user.Name);
            if (clientResult.IsSuccess == true)
                Groups = clientResult.Result;
        }
    }
}