namespace MyMD5;

public class DirectoryHashSingleThread : DirectoryHashCommon
{
    public static DirectoryHash CalculateDirectoryHash(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            throw new ArgumentException($"ACHTUNG! Given directory not exists! ({directoryPath})");
        }

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var resultHash = getDirectoryHash(directoryPath);
        stopwatch.Stop();
        var result = new DirectoryHash(directoryPath, stopwatch.ElapsedMilliseconds, resultHash, "single");
        return result;
    }
    private static byte[] hashContentSet(string directoryName,
                    List<byte[]> fileHashes, List<byte[]> directoryHashes)
    {
        var byteDirectoryName = Encoding.UTF8.GetBytes(directoryName);
        var concatenated = new byte[byteDirectoryName.Count() +
                                    fileHashes.Count() +
                                    directoryHashes.Count()];

        foreach (var c in byteDirectoryName)
        {
            concatenated.Append(c);
        }

        foreach (var fileHash in fileHashes)
        {
            foreach (var c in fileHash)
            {
                concatenated.Append(c);
            }
        }

        foreach (var directoryHash in directoryHashes)
        {
            foreach (var c in directoryHash)
            {
                concatenated.Append(c);
            }
        }

        var resultHash = MD5.HashData(concatenated);
        return resultHash;
    }

    private static byte[] getDirectoryHash(string directoryName)
    {
        var files = Directory.EnumerateFiles(directoryName);
        var directories = Directory.EnumerateDirectories(directoryName);
        var innerFileHashes = new List<byte[]>();
        var innerDirectoryHashes = new List<byte[]>();

        files.Order();
        directories.Order();

        foreach (var file in files)
        {
            innerFileHashes.Append(getFileHash(file));
        }

        foreach (var directory in directories)
        {
            innerDirectoryHashes.Append(getDirectoryHash(directory));
        }

        var directoryHash = hashContentSet(directoryName, innerFileHashes, innerDirectoryHashes);

        return directoryHash;
    }
}