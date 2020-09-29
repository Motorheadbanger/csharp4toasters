using OpenQA.Selenium;

namespace WebAddressBookTests
{
    public class NavigationHelper
    {
        private string baseUrl;

        public NavigationHelper(IWebDriver driver, string baseUrl) : base(driver)
        {
            this.baseUrl = baseUrl;
        }

        public void GoToHomePage()
        {
            driver.Navigate().GoToUrl(baseUrl);
        }

        public void GoToGroupsPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }

        public void ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
        }
    }
}
