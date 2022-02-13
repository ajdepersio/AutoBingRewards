using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBingRewards.PageModels
{
    public class SearchPageModel
    {
        private const string _baseUrl = "https://www.bing.com/search?q=";

        private IPage _page;

        private SearchPageModel(IPage page)
        {
            _page = page;
        }

        public static async Task<SearchPageModel> NavigateToAsync(IBrowserContext context, string searchText)
        {
            var page = await context.NewPageAsync().ConfigureAwait(false);
            await page.GotoAsync($"{_baseUrl}{searchText}").ConfigureAwait(false);

            return new SearchPageModel(page);
        }
    }
}
