namespace MyMD5;  

internal class MyMD5
{
    static async Task Main(string[] args)
    {
        if (args.Count() != 1)
        {
            throw new ArgumentException("ACHTUNG! Argument count should be equal 1");
        }

        var stHash = await DirectoryHashSingleThread.CalculateDirectoryHash(args[0]);
        var mtHash = await DirectoryHashMultiThread.CalculateDirectoryHash(args[0]);
        Printer.PrintHashInfo(stHash);
        Printer.PrintHashInfo(mtHash);
    }
}
