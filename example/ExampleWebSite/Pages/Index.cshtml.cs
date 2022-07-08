using BrandUp.Website.Pages;
namespace ExampleWebSite.Pages
{
    public class IndexModel : AppPageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public override string Title => "Index";

        public void OnGet()
        {

        }
    }
}