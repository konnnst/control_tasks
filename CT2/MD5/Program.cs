namespace MyMD5;  

internal class MyMD5
{
    static void Main(string[] args)
    {
        Directory.SetCurrentDirectory("/home/konnnst/Desktop/ct/");
        var stHash = DirectoryHashSingleThread.CalculateDirectoryHash("CT2");
        Printer.PrintHashInfo(stHash);
    }
}
