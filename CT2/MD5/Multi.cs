using System.Threading.Tasks;

namespace MyMD5;

public class DirectoryHashMultiThread : DirectoryHashCommon
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
        var result = new DirectoryHash(directoryPath, stopwatch.ElapsedMilliseconds, resultHash, ThreadMode.MultiThread);
        return result;
    }

    private static async Task<byte[]> getDirectoryHash(string directoryName)
    {
        var files = Directory.EnumerateFiles(directoryName).ToArray();
        var directories = Directory.EnumerateDirectories(directoryName).ToArray();
        var innerFileHashes = new byte[files.Count()][];
        var innerDirectoryHashes = new byte[directories.Count()][];

        files.Order();
        directories.Order();

        Parallel.For(0, files.Count(), async i =>
        {
            var innerFileHash = await getFileHash(files[i]);
            innerFileHashes[i] = innerFileHash;
        });

        Parallel.For(0, directories.Count(), async i =>
        {
            var innerDirectoryHash = await getDirectoryHash(directories[i]);
            innerDirectoryHashes[i] = innerDirectoryHash;
        });

        var directoryHash = hashContentSet(directoryName, innerFileHashes, innerDirectoryHashes);

        return directoryHash;
    }
}