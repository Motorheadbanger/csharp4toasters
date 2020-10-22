using OpenQA.Selenium;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebAddressBookTests
{
    public class ContactsHelper : HelperBase
    {
        private List<ContactData> contactsCache = null;

        public ContactsHelper(ApplicationManager applicationManager) : base(applicationManager)
        {
        }

        public ContactData GetContactContentFromTable(int index)
        {
            applicationManager.NavigationHelper.GoToHomePage();

            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));

            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllEmails = allEmails,
                AllPhones = allPhones
            };
        }

        public ContactData GetContactContentFromEditForm(int index)
        {
            applicationManager.NavigationHelper.GoToHomePage();
            GoToContactEditForm(index);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone
            };
        }

        public ContactsHelper AddContact(ContactData contactData)
        {
            driver.FindElement(By.LinkText("add new")).Click();
            FillContactInfo(contactData);
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            contactsCache = null;
            applicationManager.NavigationHelper.GoToHomePage();

            return this;
        }

        public List<ContactData> GetContactsList()
        {
            if (contactsCache == null)
            {
                contactsCache = new List<ContactData>();

                applicationManager.NavigationHelper.GoToHomePage();

                ICollection<IWebElement> elementsList = driver.FindElements(By.XPath("//tr[contains(@name,'entry')]"));

                foreach (var element in elementsList)
                {
                    var contactData = new ContactData("", "")
                    {
                        FirstName = element.FindElement(By.XPath("./td[3]")).Text,
                        LastName = element.FindElement(By.XPath("./td[2]")).Text
                    };

                    contactsCache.Add(contactData);
                }
            }

            return new List<ContactData>(contactsCache);
        }

        public ContactsHelper ModifyContact(int index, ContactData contactData)
        {
            if (!IsElementPresent(By.XPath("//img[@title='Edit'][" + index + 1 + "]")))
                AddContact(new ContactData("emergency", "contact"));

            GoToContactEditForm(index);
            FillContactInfo(contactData);
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
            contactsCache = null;

            return this;
        }

        public ContactsHelper RemoveContact(int index)
        {
            if (!IsElementPresent(By.XPath("//input[contains(@title,'Select')][" + index + 1 + "]")))
            {
                AddContact(new ContactData("emergency", "contact"));
            }

            driver.FindElement(By.XPath("//input[contains(@title,'Select')][" + index + 1 + "]")).Click();
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            contactsCache = null;

            return this;
        }

        public ContactsHelper GoToContactEditForm(int index)
        {
            driver.FindElement(By.XPath("//img[@title='Edit'][" + index + 1 + "]")).Click();

            return this;
        }

        public int GetSearchResultsCount()
        {
            applicationManager.NavigationHelper.GoToHomePage();

            string text = driver.FindElement(By.TagName("label")).Text;
            Match match = new Regex(@"\d+").Match(text);
            return int.Parse(match.Value);
        }

        private void FillContactInfo(ContactData contactData)
        {
            FillField(By.Name("firstname"), contactData.FirstName);
            FillField(By.Name("lastname"), contactData.LastName);
        }
    }
}
