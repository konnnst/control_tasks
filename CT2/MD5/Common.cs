namespace MyMD5;

public class DirectoryHashCommon
{
    public enum ThreadMode { SingleThread, MultiThread }
    public class DirectoryHash 
    {
        public long Time { get; }
        public byte[] Hash { get; }
        public string Path { get; }
        public ThreadMode Mode { get; }
        public DirectoryHash(string path, long time, byte[] hash, ThreadMode mode)
        {
            Path = path;
            Time = time;
            Hash = hash;
            Mode = mode;
        }
    }

    protected static async Task<byte[]> getFileHash(string fileName)
    {
        var fileByteArray = await File.ReadAllBytesAsync(fileName);
        var fileHash = MD5.HashData(fileByteArray);
        return fileHash;
    }

    protected static byte[] hashContentSet(string directoryName,
                    byte[][] fileHashes, byte[][] directoryHashes)
    {
        var byteDirectoryName = Encoding.UTF8.GetBytes(directoryName);
        var concatenated = new byte[byteDirectoryName.Count() +
                                    fileHashes.Count() * 16 +
                                    directoryHashes.Count() * 16];


        byteDirectoryName.CopyTo(concatenated, 0);
        for (var i = 0; i < fileHashes.Count(); ++i)
        {
            fileHashes[i].CopyTo(concatenated, byteDirectoryName.Count() +
                                i * 16);
        }

        for (var i = 0; i < directoryHashes.Count(); ++i)
        {
            directoryHashes[i].CopyTo(concatenated, byteDirectoryName.Count() +
                                fileHashes.Count() * 16 + i * 16);
        }

        var resultHash = MD5.HashData(concatenated);
        return resultHash;
    }
}