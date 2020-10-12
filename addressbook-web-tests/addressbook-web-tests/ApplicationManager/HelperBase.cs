using OpenQA.Selenium;

namespace WebAddressBookTests
{
    public class HelperBase
    {
        protected IWebDriver driver;
        protected ApplicationManager applicationManager;

        public HelperBase(ApplicationManager applicationManager)
        {
            this.applicationManager = applicationManager;
            driver = applicationManager.Driver;
        }

        public void FillField(By locator, string value)
        {
            if (value != null)
            {
                driver.FindElement(locator).Clear();
                driver.FindElement(locator).SendKeys(value);
            }
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
