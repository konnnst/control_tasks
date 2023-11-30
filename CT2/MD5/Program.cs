namespace MyMD5;  

internal class MyMD5
{
    static void Main(string[] args)
    {
        if (args.Count() != 1)
        {
            throw new ArgumentException("ACHTUNG! Argument count should be equal 1");
        }

        var stHash = DirectoryHashSingleThread.CalculateDirectoryHash(args[0]);
        Printer.PrintHashInfo(stHash);
    }
}
