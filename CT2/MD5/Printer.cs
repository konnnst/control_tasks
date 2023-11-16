namespace MyMD5;

public static class Printer
{
    public static void PrintHashInfo(DirectoryHashCommon.DirectoryHash hash)
    {
        Console.WriteLine($"Hashed {hash.Path} directory");
        Console.WriteLine($"Hash equals {Encoding.UTF8.GetString(hash.Hash, 0, hash.Hash.Count())}");
        Console.WriteLine($"Elapsed {hash.Time} milliseconds");
        string mode;
        if (hash.Mode == DirectoryHashCommon.ThreadMode.SingleThread)
        {
            mode = "single";
        }
        else
        {
            mode = "multi";
        }
        Console.WriteLine($"Used {mode} threading mode");
    }
}