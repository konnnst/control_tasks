namespace MyMD5;

public static class Common
{
    public static byte[] getFileHash(string fileName)
    {
        var fileByteArray = File.ReadAllBytes(fileName);
        var fileHash = MD5.HashData(fileByteArray);
        return fileHash;
    }


}