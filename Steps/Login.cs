using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoBingRewards.Steps
{
    public static class Login
    {
        public static void PerformLogin(IWebDriver driver, string username, string password = "")
        {
            var url = "https://login.live.com/login.srf";
            driver.Navigate().GoToUrl(url);
            //#i0116 -- username
            driver.FindElement(By.Id("i0116")).SendKeys(username);
            //#idSIButton9 -- username next button
            driver.FindElement(By.Id("idSIButton9")).Click();

            Thread.Sleep(500);  //Adjust as needed
            //No Password provided
            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Please enter password (in Bing) and press any key to contine...");
                Console.Read();
            }
            //Password provided
            else
            {
                //#i0118 -- password
                driver.FindElement(By.Id("i0118")).SendKeys(password);
            }
            //#idSIButton9 -- password next button
            driver.FindElement(By.Id("idSIButton9")).Click();
        }
    }
}
