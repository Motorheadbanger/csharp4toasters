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
            int count = Convert.ToInt32(args[0]);
            string fileName = args[1];
            string format = args[2];
            List<GroupData> groups = new List<GroupData>();

            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(100),
                    Footer = TestBase.GenerateRandomString(100)
                });
            }

            if (format == "xlsx")
            {
                WriteGroupsToXlsx(groups, fileName);
            }
            else
            {
                StreamWriter streamWriter = new StreamWriter(fileName);

                if (format == "csv")
                    WriteGroupsToCsv(groups, streamWriter);
                else if (format == "xml")
                    WriteGroupsToXml(groups, streamWriter);
                else if (format == "json")
                    WriteGroupsToJson(groups, streamWriter);
                else
                    Console.Out.Write("Unrecognised format " + format);

                streamWriter.Close();
            }
        }

        static void WriteGroupsToCsv(List<GroupData> groups, StreamWriter streamWriter)
        {
            foreach(GroupData group in groups)
            {
                streamWriter.WriteLine(string.Format("${0},${1},${2}",
                    group.Name,
                    group.Header,
                    group.Footer));
            }
        }

        static void WriteGroupsToXml(List<GroupData> groups, StreamWriter streamWriter)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(streamWriter, groups);
        }

        static void WriteGroupsToJson(List<GroupData> groups, StreamWriter streamWriter)
        {
            streamWriter.Write(JsonConvert.SerializeObject(groups, Formatting.Indented));
        }
        static void WriteGroupsToXlsx(List<GroupData> groups, string fileName)
        {

        }
    }
}
