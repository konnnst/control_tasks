namespace Chat;
public class MyChat
{
    static void Main(string[] args)
    {
        if (ArgModeChecker.CheckMode(args) == "client")
        {
            Parser.ParseArgs(args);
            var client = new Client(Parser.IP, Parser.Port);
            Console.WriteLine("Client is running:");
            //client.Start();
        }
        else if (ArgModeChecker.CheckMode(args) == "server")
        {
            Parser.ParseArgs(args);
            var server = new Server(Parser.Port);
            Console.WriteLine("Server is running:");
        //    server.Start();
        }
        else
        {
            Console.WriteLine("Achtung! Incorrect input");
        }
        Console.ReadKey();
    }
}