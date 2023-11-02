namespace Chat;

public static class Parser
{
    private static int _port;
    private static IPAddress? _IP;

    public static int Port => _port;
    public static IPAddress? IP=> _IP;

    /// <summary>
    /// Gets port and IP (if noted) from correct string to class properties
    /// </summary>
    /// <param name="args">command line arguments</param>
    public static void ParseArgs(string[] args)
    {
        var argsCount = args.Count<string>();

        if (argsCount == 2)
        {
            IPAddress.TryParse(args[0], out IPAddress parsedIP);
            _IP = parsedIP;
            Int32.TryParse(args[1], out int parsedPort);
            _port = parsedPort;
        }
    }
}