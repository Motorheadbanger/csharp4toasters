using Excel = Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthenticationTestBase
    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });
            }

            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromCsv()
        {
            List<GroupData> groups = new List<GroupData>();
            string[] lines = File.ReadAllLines(@"groups.csv");

            foreach (string line in lines)
            {
                string[] items = line.Split(',');
                groups.Add(new GroupData(items[0])
                {
                    Header = items[1],
                    Footer = items[2]
                });
            }

            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromXml()
        {
            return (List<GroupData>) new XmlSerializer(typeof(List<GroupData>)).Deserialize(new StreamReader(@"groups.xml"));
        }

        public static IEnumerable<GroupData> GroupDataFromJson()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(File.ReadAllText(@"groups.json"));
        }

        public static IEnumerable<GroupData> GroupDataFromXlsx()
        {
            List<GroupData> groups = new List<GroupData>();

            Excel.Application application = new Excel.Application();
            Excel.Workbook workBook = application.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"groups.xlsx"));
            Excel.Worksheet sheet = workBook.ActiveSheet;
            Excel.Range range = sheet.UsedRange;
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    Name = range.Cells[i, 1].Value,
                    Header = range.Cells[i, 2].Value,
                    Footer = range.Cells[i, 3].Value
                });
            }

            workBook.Close();
            application.Quit();

            return groups;
        }

        [Test, TestCaseSource("GroupDataFromXlsx")]
        public void GroupCreationTest(GroupData group)
        {
            List<GroupData> initialGroupsList = applicationManager.GroupHelper.GetGroupsList();

            applicationManager.GroupHelper.Create(group);

            List<GroupData> modifiedGroupsList = applicationManager.GroupHelper.GetGroupsList();

            Assert.AreEqual(initialGroupsList.Count + 1, applicationManager.GroupHelper.GetGroupsCount());

            initialGroupsList.Add(group);
            initialGroupsList.Sort();
            modifiedGroupsList.Sort();

            Assert.AreEqual(initialGroupsList, modifiedGroupsList);
        }
    }
}
