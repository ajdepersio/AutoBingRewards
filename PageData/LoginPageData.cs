using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBingRewards.PageData
{
    internal static class LoginPageData
    {
        private const string _usernameTextBoxSelector = "i0116";
        private const string _usernameNextButtonSelector = "idSIButton9";
        private const string _passwordTextBoxSelector = "i0118";
        private const string _passwordNextButtonSelector = "idSIButton9";

        internal static By UsernameTextBox = By.Id(_usernameTextBoxSelector);
        internal static By UsernameNextButton = By.Id(_usernameNextButtonSelector); 
        internal static By PasswordTextBox = By.Id(_passwordTextBoxSelector);
        internal static By PasswordNextButton = By.Id(_passwordNextButtonSelector);
    }
}
