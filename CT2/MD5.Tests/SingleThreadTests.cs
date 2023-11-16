using MyMD5;

namespace MD5.Tests;

[TestClass]
public class SingleThreadTest
{
    [TestMethod]
    public async void MultipleTimeHashingTest()
    {
        var writer = new StreamWriter("/home/konnnst/Desktop/res.txt");
        Directory.SetCurrentDirectory("../../../../");
        var hash1 = Encoding.UTF8.GetString((await DirectoryHashSingleThread.CalculateDirectoryHash("resources")).Hash);
        var hash2 = Encoding.UTF8.GetString((await DirectoryHashSingleThread.CalculateDirectoryHash("resources")).Hash);
        Assert.AreEqual(hash1, hash2);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task IsFileNotExistExceptionThrownTest()
    {
        await DirectoryHashSingleThread.CalculateDirectoryHash("fjaskdfj");
    }
}