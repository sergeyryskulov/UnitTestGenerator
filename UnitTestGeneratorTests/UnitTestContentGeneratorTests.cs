using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGenerator.Tests
{
    [TestClass()]
    public class UnitTestContentGeneratorTests
    {
        
        [TestMethod()]
        public void GenerateUnitTestContentTest()
        {
            var unitTestContentGenerator = CreateUnitTestContentGenerator();

            var expected = File.ReadAllText("GenerateContentTest.txt");

            var actual = unitTestContentGenerator.GenerateUnitTestContent("TestClass", new List<string>()
            {
                "ITestInterface1",
                "ITestInterface2",
            });

            Assert.AreEqual(Trim(expected), Trim(actual));
        }

        private string Trim(string content)
        {
            return content.Replace("\n", "").Replace("\r", "").Replace(" ", "");
        }
        private UnitTestContentGenerator CreateUnitTestContentGenerator()
        {
            return new UnitTestContentGenerator();
        }
    }
}