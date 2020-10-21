using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;

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

        private static readonly ThreadLocal<ApplicationManager> applicationManager = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            Driver = new FirefoxDriver();
            BaseUrl = "http://localhost/addressbook";
            LoginHelper = new LoginHelper(this);
            NavigationHelper = new NavigationHelper(this, BaseUrl);
            GroupHelper = new GroupHelper(this);
            ContactsHelper = new ContactsHelper(this);
        }

        public static ApplicationManager GetInstance()
        {
            if (!applicationManager.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.NavigationHelper.GoToHomePage();
                applicationManager.Value = newInstance;

            }

            return applicationManager.Value;
        }

        ~ApplicationManager()
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
