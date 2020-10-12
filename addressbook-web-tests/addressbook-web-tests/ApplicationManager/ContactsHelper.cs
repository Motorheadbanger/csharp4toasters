using OpenQA.Selenium;

namespace WebAddressBookTests
{
    public class ContactsHelper : HelperBase
    {
        public ContactsHelper(ApplicationManager applicationManager) : base(applicationManager)
        {
        }

        public ContactsHelper AddContact(ContactData contactData)
        {
            driver.FindElement(By.LinkText("add new")).Click();
            FillContactInfo(contactData);
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            applicationManager.NavigationHelper.GoToHomePage();
            return this;
        }

        public ContactsHelper ModifyContact(int index, ContactData contactData)
        {
            if (!IsElementPresent(By.XPath("//img[@title='Edit'][" + index + "]")))
                AddContact(new ContactData("emergency", "contact"));

            driver.FindElement(By.XPath("//img[@title='Edit'][" + index + "]")).Click();
            FillContactInfo(contactData);
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
            return this;
        }

        public ContactsHelper RemoveContact(int index)
        {
            if (!IsElementPresent(By.XPath("//input[contains(@title,'Select')][" + index + "]")))
            {
                AddContact(new ContactData("emergency", "contact"));
            }

            driver.FindElement(By.XPath("//input[contains(@title,'Select')][" + index + "]")).Click();
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        private void FillContactInfo(ContactData contactData)
        {
            FillField(By.Name("firstname"), contactData.FirstName);
            FillField(By.Name("lastname"), contactData.LastName);
        }
    }
}
