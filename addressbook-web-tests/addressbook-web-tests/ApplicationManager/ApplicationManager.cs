using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;

namespace WebAddressBookTests
{
    public class ApplicationManager
    {
        public IWebDriver Driver { get; }
        public string BaseUrl { get; }
        public LoginHelper LoginHelper { get; }
        public NavigationHelper NavigationHelper { get; }
        public GroupHelper GroupHelper { get; }
        public ContactsHelper ContactsHelper { get; }

        public ApplicationManager()
        {
            Driver = new FirefoxDriver();
            BaseUrl = "http://localhost";
            LoginHelper = new LoginHelper(Driver);
            NavigationHelper = new NavigationHelper(Driver, BaseUrl);
            GroupHelper = new GroupHelper(Driver);
            ContactsHelper = new ContactsHelper(Driver);
        }

        public void Stop()
        {
            try
            {
                Driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
    }
}
