using System.Threading;
using OpenQA.Selenium;

namespace WebAddressBookTests
{
    public class LoginHelper
    {
        public LoginHelper(IWebDriver driver) : base(driver)
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
            Thread.Sleep(100);
        }
    }
}
