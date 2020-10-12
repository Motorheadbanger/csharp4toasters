using System;
using System.Threading;
using OpenQA.Selenium;

namespace WebAddressBookTests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager applicationManager) : base(applicationManager)
        {
        }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                    return;

                Logout();
            }
            FillField(By.Name("user"), account.Username);
            FillField(By.Name("pass"), account.Password);
            driver.FindElement(By.Id("LoginForm")).Submit();
            Thread.Sleep(200);
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.Name("logout"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            Thread.Sleep(200);
            if (!IsLoggedIn()) return false;

            return driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text == "(" + account.Username + ")";
        }

        public void Logout()
        {
            if (IsLoggedIn())
                driver.FindElement(By.LinkText("Logout")).Click();
        }
    }
}
