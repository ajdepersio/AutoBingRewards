using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBingRewards.PageModels
{
    public class LoginPageModel
    {
        private const string _baseUrl = "https://login.live.com/login.srf";

        private const string _usernameTextBoxSelector = "#i0116";
        private const string _usernameNextButtonSelector = "#idSIButton9";
        private const string _passwordTextBoxSelector = "#i0118";
        private const string _passwordNextButtonSelector = "#idSIButton9";

        private IPage _page;

        private LoginPageModel(IPage page)
        {
            _page = page;
        }

        public static async Task<LoginPageModel> NavigateToAsync(IBrowserContext context)
        {
            var page = await context.NewPageAsync().ConfigureAwait(false);
            await page.GotoAsync(_baseUrl).ConfigureAwait(false);

            return new LoginPageModel(page);
        }

        public async Task EnterUsername(string username)
        {
            await _page.FillAsync(_usernameTextBoxSelector, username).ConfigureAwait(false);
            await _page.ClickAsync(_usernameNextButtonSelector).ConfigureAwait(false);
        }

        public async Task EnterPassword(string password)
        {
            await _page.FillAsync(_passwordTextBoxSelector, password).ConfigureAwait(false);
            await _page.ClickAsync(_passwordNextButtonSelector).ConfigureAwait(false);
        }
    }
}
