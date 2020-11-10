using NUnit.Framework;
using System;
using System.Text;

namespace WebAddressBookTests
{
    public class TestBase
    {
        protected ApplicationManager applicationManager;
        public static Random random = new Random();
        public static bool PERFORM_LONG_UI_CHECKS = true;

        [SetUp]
        public void SetupApplicationManager()
        {
            applicationManager = ApplicationManager.GetInstance();
        }

        public static string GenerateRandomString(int length)
        {
            int l = Convert.ToInt32(random.NextDouble() * length);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < l; i++)
            {
                stringBuilder.Append(Convert.ToChar(32 + Convert.ToInt32(random.NextDouble() * 65)));
            }

            return stringBuilder.ToString();
        }
    }
}
