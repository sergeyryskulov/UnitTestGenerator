
class Program

{
    static string FirstLetterDown(string className)
    {
        return className[0].ToString().ToLower() + className.Substring(1);
    }

    static string GenerateUnitTest(
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

    static void Main(string[] args)
    {
        string mainClass = args[0];
        var interfaces = args.ToList();
        interfaces.RemoveAt(0);

        string generatedContent = GenerateUnitTest(mainClass, interfaces);

        File.WriteAllText("out.txt", generatedContent);
    }
}