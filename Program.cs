﻿using System;
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
        //private static IBrowser _browser;

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
            for (int i = 0; i < settings.Usernames.Count(); i++)
            {
                var username = settings.Usernames[i];
                var password = settings.Passwords[i];

                await MobileSearches(playwright, username, password).ConfigureAwait(false);
                await DesktopSearches(playwright, username, password).ConfigureAwait(false);
            }

            //var page = await _browser.NewPageAsync();
            //await page.GotoAsync("https://playwright.dev/dotnet");
            //await page.ScreenshotAsync(new PageScreenshotOptions { Path = "screenshot.png" });

            //SetupDriver();
            ////Console.WriteLine("Please sign into Bing Rewards account and press any key to contine...");
            ////Console.Read();
            //Login.PerformLogin(_webDriver, args[0] ?? "",args[1] ?? "");
            //DailySearches();
            //DailyRewardSet();
            //AdditionalOffers();
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
            Parallel.For(0, 30, (_, __) =>
            //for(int i = 0; i < 30; i++)
            {
                var searchPage = SearchPageModel.NavigateToAsync(context, random.Next().ToString()).Result;
            }
            );
            context.CloseAsync().Wait();
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
            Parallel.For(0, 20, (_, __) =>
            //for (int i = 0; i < 20; i++)
            {
                var searchPage = SearchPageModel.NavigateToAsync(context, random.Next().ToString()).Result;
            }
            );
            context.CloseAsync().Wait();
        }

        static void DailySearches()
        {
            ////Level 2 search allows for 150 points per day on desktop searches.  5 points per search
            ////TODO: Stop automatically once we've completed all available search rewards
            //for (int i = 0; i < 100; i++)
            //{
            //    Search.PerformSearch(_webDriver, i.ToString());
            //}
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
