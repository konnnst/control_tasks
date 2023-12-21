namespace Reflector.Tests;

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
