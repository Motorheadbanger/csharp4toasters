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
            driver.FindElement(By.Name("user")).Click();
            driver.FindElement(By.Name("user")).Clear();
            driver.FindElement(By.Name("user")).SendKeys(account.Username);
            driver.FindElement(By.Name("pass")).Clear();
            driver.FindElement(By.Name("pass")).SendKeys(account.Password);
            driver.FindElement(By.Id("LoginForm")).Submit();
            Thread.Sleep(200);
        }
    }
}
