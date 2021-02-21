using System;
using System.IO;
using AutoBingRewards.Steps;
using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace AutoBingRewards
{
    class Program
    {
        private static IWebDriver _webDriver;

        static void Main(string[] args)
        {
            SetupDriver();
            //Console.WriteLine("Please sign into Bing Rewards account and press any key to contine...");
            //Console.Read();
            Login.PerformLogin(_webDriver, args[0] ?? "",args[1] ?? "");
            DailySearches();
            //DailyRewardSet();
            //AdditionalOffers();
        }

        private static void SetupDriver()
        {
            new DriverManager().SetUpDriver(new EdgeConfig());
            //Get directory for driver executable
            var driverPath = Directory.GetDirectories(Directory.GetCurrentDirectory() + "\\Edge\\")[0] + "\\X64";
            _webDriver = new EdgeDriver(driverPath, new EdgeOptions() { UseChromium = true });

            _webDriver.Navigate().GoToUrl("https://bing.com");
        }

        static void DailySearches()
        {
            //Level 2 search allows for 150 points per day on desktop searches.  5 points per search
            //TODO: Stop automatically once we've completed all available search rewards
            for (int i = 0; i < 100; i++)
            {
                Search.PerformSearch(_webDriver, i.ToString());
            }
        }
        private static void DailyRewardSet()
        {
            _webDriver.Navigate().GoToUrl("https://account.microsoft.com/rewards");
            //TODO: Filter out already completed offers
            var dailyRewardsLinks = _webDriver.FindElements(By.CssSelector("mee-rewards-daily-set-item-content .actionLink a"));

            //HACK: Rework the query selector, for now, the daily set should be index 1, 3, and 5
            for (int i = 1; i < 5; i+=2)
            {
                dailyRewardsLinks[i].Click();
                Console.WriteLine("Once offer is complete, press any key to contine...");
                Console.Read();
            }
        }
        private static void AdditionalOffers()
        {
            //TODO: Provide implementation
            //throw new NotImplementedException();
        }
    }
}
