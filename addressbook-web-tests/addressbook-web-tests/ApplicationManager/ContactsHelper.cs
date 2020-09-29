using OpenQA.Selenium;

namespace WebAddressBookTests
{
    public class ContactsHelper
    {
        public ContactsHelper(IWebDriver driver) : base(driver)
        {
        }

        public void AddContact(ContactData contactData)
        {
            driver.FindElement(By.LinkText("add new")).Click();
            driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contactData.FirstName);
            driver.FindElement(By.Name("lastname")).Click();
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contactData.LastName);
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
        }
    }
}
