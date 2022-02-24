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
        public IPage Page { get; set; }

        private SearchPageModel(IPage page)
        {
            Page = page;
        }


        public static async Task<SearchPageModel> NavigateToAsync(IBrowserContext context, string searchText)
        {
            var page = await context.NewPageAsync().ConfigureAwait(false);
            await page.GotoAsync($"{_baseUrl}{searchText}").ConfigureAwait(false);

            return new SearchPageModel(page);
        }
    }
}
