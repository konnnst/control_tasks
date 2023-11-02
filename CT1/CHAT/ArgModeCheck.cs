namespace Chat;

/// <summary>
/// Checking working mode from cli arguments
/// </summary>
public static class ArgModeChecker
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args">Cli arg string</param>
    /// <returns> server if only port given,
    /// client if port and IP given,
    /// error if input is incorrect in any way</returns>
    public static string CheckMode(string[] args)
    {
        var argsCount = args.Count<string>();

        if (argsCount == 2)
        {
            var givenIP = args[0];
            var givenPort = args[1];

            if (!Int32.TryParse(givenPort, out int port) && !IPAddress.TryParse(givenIP, out IPAddress IP))
            {
                return "error";
            }

            if (port < 0 || port > 65535)
            {
                return "error";
            }

            return "client";
        }

        else if (argsCount == 1)
        {
            var givenPort = args[0];

            if (!Int32.TryParse(givenPort, out int port))
            {
                return "error";
            }

            if (port < 0 || port > 65536)
            {
                return "error";
            }

            return "server";
        }
        else
        {
            return "error";
        }
    }
}