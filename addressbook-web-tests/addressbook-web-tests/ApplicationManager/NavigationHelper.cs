using OpenQA.Selenium;

namespace WebAddressBookTests
{
    public class NavigationHelper : HelperBase
    {
        private string baseUrl;

        public NavigationHelper(ApplicationManager applicationManager, string baseUrl) : base(applicationManager)
        {
            this.baseUrl = baseUrl;
        }

        public void GoToHomePage()
        {
            if (driver.Url == baseUrl)
                return;

            driver.Navigate().GoToUrl(baseUrl);
        }

        public void GoToGroupsPage()
        {
            if (driver.Url == baseUrl + "group.php" && IsElementPresent(By.Name("new")))
                return;

            driver.FindElement(By.LinkText("groups")).Click();
        }

        public void ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home")).Click();
        }
    }
}
