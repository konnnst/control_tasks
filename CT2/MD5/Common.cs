namespace MyMD5;

public class DirectoryHashCommon
{
    public class DirectoryHash 
    {
        public long Time { get; }
        public byte[] Hash { get; }
        public string Path { get; }
        public string Mode { get; }
        public DirectoryHash(string path, long time, byte[] hash, string mode)
        {
            Path = path;
            Time = time;
            Hash = hash;
            Mode = mode;
        }
    }
    public static byte[] getFileHash(string fileName)
    {
        var fileByteArray = File.ReadAllBytes(fileName);
        var fileHash = MD5.HashData(fileByteArray);
        return fileHash;
    }
}