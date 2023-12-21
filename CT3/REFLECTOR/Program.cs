namespace Reflector;

public class Number
{
    public int number { get; private set; }
    public Number(int num)
    {
        number = num;
    }

    public Number Add(Number B)
    {
        return new Number(B.number + number);
    }
}

internal class MyReflector
{
    static void Main()
    {
        var reflector = new Reflector();
        reflector.PrintStructure(typeof(Number));
        Console.ReadLine();
    }
}