using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGenerator
{
    public class UnitTestContentGenerator
    {
        public string GenerateUnitTestContent (
            string className,
            List<string> interfaceNames)
        {
            string result = "";
            foreach (var fieldClass in interfaceNames)
            {
                result += $"private Mock<{fieldClass}> _{FirstLetterDown(fieldClass.Substring(1))};\n";
            }

            result += "\n[TestInitialize]\npublic void InitMocks()\n{\n";

            foreach (var fieldClass in interfaceNames)
            {
                result += $"_{FirstLetterDown(fieldClass.Substring(1))} = new Mock<{fieldClass}>();\n";
            }

            result += "}\n\n";

            result += $"private {className} Create{className}()\n";
            result += "{\n";
            result += $"return new {className}(\n";

            foreach (var fieldClass in interfaceNames)
            {

                result += $"_{FirstLetterDown(fieldClass.Substring(1))}.Object";
                if (fieldClass != interfaceNames.Last())
                {
                    result += ",\n";
                }
                else
                {
                    result += "\n";
                }
            }

            result += ");\n";
            result += "}\n";
            result += "\n\n";
            result += "void Test()\n";
            result += "{\n";
            result += $"var {FirstLetterDown(className)} = Create{className}(); \n";
            result += "}\n";

            return result;
        }
        private string FirstLetterDown(string className)
        {
            return className[0].ToString().ToLower() + className.Substring(1);
        }
    }
}
