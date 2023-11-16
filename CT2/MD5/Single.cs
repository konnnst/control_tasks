namespace MyMD5;

public class DirectoryHashSingleThread : DirectoryHashCommon
{
    public static async Task<DirectoryHash> CalculateDirectoryHash(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            throw new ArgumentException($"ACHTUNG! Given directory not exists! ({directoryPath})");
        }

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var resultHash = await getDirectoryHash(directoryPath);
        stopwatch.Stop();
        var result = new DirectoryHash(directoryPath, stopwatch.ElapsedMilliseconds, resultHash, ThreadMode.SingleThread);
        return result;
    }

    private static async Task<byte[]> getDirectoryHash(string directoryName)
    {
        var files = Directory.GetFiles(directoryName);
        var directories = Directory.GetDirectories(directoryName);
        var innerFileHashes = new byte[files.Count()][];
        var innerDirectoryHashes = new byte[directories.Count()][];

        files.Order();
        directories.Order();

        for (var i = 0; i < files.Count(); ++i)
        {
            var innerFileHash = await getFileHash(files[i]);
            innerFileHashes[i] = innerFileHash;
        }

        for (var i = 0; i < directories.Count(); ++i)
        {
            var innerDirectoryHash = await getDirectoryHash(directories[i]);
            innerDirectoryHashes[i] = innerDirectoryHash;
        }

        var directoryHash = hashContentSet(directoryName, innerFileHashes, innerDirectoryHashes);

        return directoryHash;
    }
}