using Reflector; 
namespace Reflector.Tests;

[TestClass]
public class PrintStructureTest
{
    [TestMethod]
    public void PrintMultiplicatorTest()
    {
        var reflector = new Reflector("../../../ReflectedClasses");
        reflector.PrintStructure(typeof(Multiplicator));
    }

    [TestMethod]
    public void PrintSummatorTest()
    {
        var reflector = new Reflector("../../../ReflectedClasses");
        reflector.PrintStructure(typeof(Summator));
    }
}