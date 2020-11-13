using NUnit.Framework;
using System.Text;

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

        [Test]
        public void ContactDetailsTest()
        {
            ContactData dataFromEditTable = applicationManager.ContactsHelper.GetContactContentFromEditForm(1);
            string dataFromDetails = applicationManager.ContactsHelper.GetContactContentFromDetails(1);
            StringBuilder formattedString = new StringBuilder();

            if (dataFromEditTable.FirstName != "")
                formattedString.Append(dataFromEditTable.FirstName + " ");

            if (dataFromEditTable.MiddleName != "")
                formattedString.Append(dataFromEditTable.MiddleName + " ");

            if (dataFromEditTable.LastName != "")
                formattedString.Append(dataFromEditTable.LastName + "\r\n");

            if (dataFromEditTable.Nickname != "")
                formattedString.Append(dataFromEditTable.Nickname + "\r\n");

            if (dataFromEditTable.Title != "")
                formattedString.Append(dataFromEditTable.Title + "\r\n");

            if (dataFromEditTable.Company != "")
                formattedString.Append(dataFromEditTable.Company + "\r\n");

            if (dataFromEditTable.Address != "")
                formattedString.Append(dataFromEditTable.Address + "\r\n\r\n");

            if (dataFromEditTable.HomePhone != "")
                formattedString.Append("H: " + dataFromEditTable.HomePhone + "\r\n");

            if (dataFromEditTable.MobilePhone != "")
                formattedString.Append("M: " + dataFromEditTable.MobilePhone + "\r\n");

            if (dataFromEditTable.WorkPhone != "")
                formattedString.Append("W: " + dataFromEditTable.WorkPhone + "\r\n");

            if (dataFromEditTable.Fax != "")
                formattedString.Append("F: " + dataFromEditTable.Fax + "\r\n\r\n");

            if (dataFromEditTable.Email1 != "")
                formattedString.Append(dataFromEditTable.Email1 + "\r\n");

            if (dataFromEditTable.Email2 != "")
                formattedString.Append(dataFromEditTable.Email2 + "\r\n");

            if (dataFromEditTable.Email3 != "")
                formattedString.Append(dataFromEditTable.Email3 + "\r\n");

            if (dataFromEditTable.Homepage != "")
                formattedString.Append("Homepage:" + "\r\n" + dataFromEditTable.Homepage + "\r\n\r\n\r\n");

            if (dataFromEditTable.SecondaryAddress != "")
                formattedString.Append(dataFromEditTable.SecondaryAddress + "\r\n\r\n");

            if (dataFromEditTable.Home != "")
                formattedString.Append("P: " + dataFromEditTable.Home + "\r\n\r\n");

            if (dataFromEditTable.Notes != "")
                formattedString.Append(dataFromEditTable.Notes);

            Assert.AreEqual(dataFromDetails, formattedString.ToString());
        }
    }
}
