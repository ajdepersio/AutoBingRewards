using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBingRewards.PageModels
{
    public class DashboardPageModel
    {
        private const string _baseUrl = "https://rewards.bing.com/";
        public IPage Page { get; set; }

        private DashboardPageModel(IPage page)
        {
            Page = page;
        }


        public static async Task<DashboardPageModel> NavigateToAsync(IBrowserContext context)
        {
            var page = await context.NewPageAsync().ConfigureAwait(false);
            await page.GotoAsync($"{_baseUrl}").ConfigureAwait(false);

            return new DashboardPageModel(page);
        }
    }
}
