using Excel = Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using WebAddressBookTests;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataType = args[0];
            int count = Convert.ToInt32(args[1]);
            string fileName = args[2];
            string format = args[3];

            if (dataType == "group")
                GenerateGroupsToFile(count, fileName, format);
            else if (dataType == "contacts")
                GenerateContactsToFile(count, fileName, format);
            else
                Console.Out.Write("Unrecognised data type " + format);
        }

        static void GenerateGroupsToFile(int count, string fileName, string format)
        {
            List<GroupData> groups = new List<GroupData>();

            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(10),
                    Footer = TestBase.GenerateRandomString(10)
                });
            }

            StreamWriter streamWriter = new StreamWriter(fileName);

            if (format == "xml")
                WriteGroupsToXml(groups, streamWriter);
            else if (format == "json")
                WriteGroupsToJson(groups, streamWriter);
            else
                Console.Out.Write("Unrecognised format " + format);

            streamWriter.Close();
        }

        static void GenerateContactsToFile(int count, string fileName, string format)
        {
            List<ContactData> contacts = new List<ContactData>();

            for (int i = 0; i < count; i++)
            {
                contacts.Add(new ContactData(TestBase.GenerateRandomString(10), TestBase.GenerateRandomString(10)));
            }

            StreamWriter streamWriter = new StreamWriter(fileName);

            if (format == "xml")
                WriteContactsToXml(contacts, streamWriter);
            else if (format == "json")
                WriteContactsToJson(contacts, streamWriter);
            else
                Console.Out.Write("Unrecognised format " + format);

            streamWriter.Close();
        }

        static void WriteGroupsToXml(List<GroupData> groups, StreamWriter streamWriter)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(streamWriter, groups);
        }

        static void WriteGroupsToJson(List<GroupData> groups, StreamWriter streamWriter)
        {
            streamWriter.Write(JsonConvert.SerializeObject(groups, Formatting.Indented));
        }

        static void WriteContactsToXml(List<ContactData> contacts, StreamWriter streamWriter)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(streamWriter, contacts);
        }

        static void WriteContactsToJson(List<ContactData> contacts, StreamWriter streamWriter)
        {
            streamWriter.Write(JsonConvert.SerializeObject(contacts, Formatting.Indented));
        }
    }
}
