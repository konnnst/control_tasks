namespace Reflector;

public class Summator
{
    private int _a;
    private int _b;
    private int? _sum = null;

    public Summator(int a, int b)
    {
        _a = a;
        _b = b;
    }

    public void Sum()
    {
        _sum = _a * _b;
    }

    public int GetResult()
    {
        if (_sum != null)
            return (int)_sum;
        throw new Exception("Not calculated");
    }
}

public class Multiplicator
{
        private int _a;
    private int _b;
    private int? _mult = null;

    public Multiplicator(int a, int b)
    {
        _a = a;
        _b = b;
    }

    public void Mult()
    {
        _mult = _a * _b;
    }

    public int GetResult()
    {
        if (_mult != null)
            return (int)_mult;
        throw new Exception("Not calculated");
    }
}

internal class MyReflector
{
    static void Main()
    {
        var reflector = new Reflector();
        reflector.PrintStructure(typeof(Summator));
        reflector.PrintStructure(typeof(Multiplicator));
        reflector.DiffClasses(typeof(Multiplicator), typeof(Summator));
        Console.ReadLine();
    }
}