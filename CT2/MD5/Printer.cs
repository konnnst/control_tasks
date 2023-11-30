namespace MyMD5;

public static class Printer
{
    public static void PrintHashInfo(DirectoryHashCommon.DirectoryHash hash)
    {
        Console.WriteLine($"Hashed {hash.Path} directory");
        Console.WriteLine($"Hash equals {Encoding.UTF8.GetString(hash.Hash, 0, hash.Hash.Count())}");
        Console.WriteLine($"Elapsed {hash.Time} milliseconds");
        Console.WriteLine($"Used {hash.Mode} threading mode");
    }
}