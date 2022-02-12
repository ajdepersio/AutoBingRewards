using AutoBingRewards.PageData;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBingRewards.PageModels
{
    public class LoginPageModel
    {
        private const string _pageUrl = "https://login.live.com/login.srf";
        public IWebDriver Driver { get; }

        public LoginPageModel(IWebDriver driver)
        {
            this.Driver = driver;
        }

        public static LoginPageModel NavigateTo(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(_pageUrl);

            return new LoginPageModel(driver);
        }

        public void EnterUsername(string username)
        {
            this.Driver.FindElement(LoginPageData.UsernameTextBox).SendKeys(username);
            this.Driver.FindElement(LoginPageData.UsernameNextButton).Click();
        }

        public void EnterPassword(string password)
        {
            this.Driver.FindElement(LoginPageData.PasswordTextBox).SendKeys(password);
            this.Driver.FindElement(LoginPageData.PasswordNextButton).Click();
        }
    }
}
