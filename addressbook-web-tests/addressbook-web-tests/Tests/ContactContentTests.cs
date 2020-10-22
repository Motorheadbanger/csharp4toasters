using NUnit.Framework;

namespace WebAddressBookTests
{
    class ContactContentTests : AuthenticationTestBase
    {
        [Test]
        public void ContactMainPageContentTest()
        {
            ContactData dataFromTable = applicationManager.ContactsHelper.GetContactContentFromTable(0);
            ContactData dataFromEditForm = applicationManager.ContactsHelper.GetContactContentFromEditForm(0);

            Assert.AreEqual(dataFromTable, dataFromEditForm);
            Assert.AreEqual(dataFromTable.Address, dataFromEditForm.Address);
            Assert.AreEqual(dataFromTable.AllPhones, dataFromEditForm.AllPhones);
        }
    }
}
