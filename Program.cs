using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoBingRewards.PageModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;

namespace AutoBingRewards
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Program>()
                .Build();

            var settings = new ApplicationSettings();
            configuration.Bind(settings);

            using var playwright = await Playwright.CreateAsync().ConfigureAwait(false);
            {
                //Parallel.For(0, settings.Usernames.Length, (i) =>
                for (int i = 0; i < settings.Usernames.Length; i++)
                {
                    var username = settings.Usernames[i];
                    var password = settings.Passwords[i];

                    MobileSearches(playwright, username, password).Wait();
                    DesktopSearches(playwright, username, password).Wait();
                }
                //);

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        private static async Task DesktopSearches(IPlaywright playwright, string username, string password)
        {
            var chromium = playwright.Chromium;
            var browser = await chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Channel = "msedge",
                Headless = false
            }).ConfigureAwait(false);

            var context = await browser.NewContextAsync().ConfigureAwait(false);

            var loginPage = await LoginPageModel.NavigateToAsync(context).ConfigureAwait(false);

            await loginPage.EnterUsername(username).ConfigureAwait(false);
            await loginPage.EnterPassword(password).ConfigureAwait(false);

            var random = new Random();
            //Parallel.For(0, 30, (_, __) =>
            for(int i = 0; i < 60; i++)
            {
                var searchPage = SearchPageModel.NavigateToAsync(context, random.Next().ToString()).Result;
                //searchPage.Page.CloseAsync();
            }
            //);
            for (int i = 1; i < context.Pages.Count; i++)
            {
                context.Pages[i].CloseAsync().Wait();
            }
            //context.CloseAsync().Wait();
        }

        private static async Task MobileSearches(IPlaywright playwright, string username, string password)
        {
            var chromium = playwright.Chromium;
            var browser = await chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            }).ConfigureAwait(false);

            var options = new BrowserNewContextOptions(playwright.Devices["iPhone 13 Pro"]);
            var context = await browser.NewContextAsync(options).ConfigureAwait(false);

            var loginPage = await LoginPageModel.NavigateToAsync(context).ConfigureAwait(false);

            await loginPage.EnterUsername(username).ConfigureAwait(false);
            await loginPage.EnterPassword(password).ConfigureAwait(false);

            var random = new Random();
            //Parallel.For(0, 20, (_, __) =>
            for (int i = 0; i < 40; i++)
            {
                var searchPage = SearchPageModel.NavigateToAsync(context, random.Next().ToString()).Result;
                //searchPage.Page.CloseAsync();
            }
            //);
            context.CloseAsync().Wait();
        }

        private static void DailyRewardSet()
        {
            //_webDriver.Navigate().GoToUrl("https://account.microsoft.com/rewards");
            ////TODO: Filter out already completed offers
            //var dailyRewardsLinks = _webDriver.FindElements(By.CssSelector("mee-rewards-daily-set-item-content .actionLink a"));

            ////HACK: Rework the query selector, for now, the daily set should be index 1, 3, and 5
            //for (int i = 1; i < 5; i+=2)
            //{
            //    dailyRewardsLinks[i].Click();
            //    Console.WriteLine("Once offer is complete, press any key to contine...");
            //    Console.Read();
            //}
        }
        private static void AdditionalOffers()
        {
            //TODO: Provide implementation
            //throw new NotImplementedException();
        }
    }
}
