using System;
using System.IO;
using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace AutoBingRewards
{
    class Program
    {
        private static  IWebDriver _webDriver;

        static void Main(string[] args)
        {
            SetupDriver();
            Console.WriteLine("Please sign into Bing Rewards account and press any key to contine...");
            Console.ReadKey();
            DailySearches();
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
            for (int i = 0; i < 30; i++)
            {
                _webDriver.Navigate().GoToUrl($"https://www.bing.com/search?q={i}");
            }
        }
    }
}
