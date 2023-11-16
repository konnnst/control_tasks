namespace MyMD5;

[TestClass]
public class CompareModes
{
    [TestMethod]
    public async Task AreMultAndSingleThreadResultsEqual()
    {
        Directory.SetCurrentDirectory("../../../../");
        var stHash = await DirectoryHashSingleThread.CalculateDirectoryHash("resources");
        var mtHash = await DirectoryHashSingleThread.CalculateDirectoryHash("resources");

        Assert.AreEqual(Encoding.UTF8.GetString(stHash.Hash), Encoding.UTF8.GetString(mtHash.Hash));
    }
}