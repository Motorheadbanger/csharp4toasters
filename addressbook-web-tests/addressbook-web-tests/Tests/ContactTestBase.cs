using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressBookTests
{
    public class ContactTestBase : AuthenticationTestBase
    {
        [TearDown]
        public void CompareContactsUI_DB()
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                List<ContactData> fromUI = applicationManager.ContactsHelper.GetContactsList();
                List<ContactData> fromDb = ContactData.GetContactsListFromDb();

                fromUI.Sort();
                fromDb.Sort();

                Assert.AreEqual(fromUI, fromDb);
            }
        }
    }
}
