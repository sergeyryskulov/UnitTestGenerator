
using UnitTestGenerator;

class Program
{
    static void Main(string[] args)
    {
        string mainClass = args[0];
        var interfaces = args.ToList();
        interfaces.RemoveAt(0);

        string generatedContent = new UnitTestContentGenerator().GenerateUnitTestContent(mainClass, interfaces);

        File.WriteAllText("out.txt", generatedContent);
    }
}