namespace Chat;

public static class TcpIO
{
    public static void Writer(NetworkStream stream, CancellationToken token)
    {
        Task.Run(async () =>
        {
            var writer = new StreamWriter(stream) { AutoFlush = true };

            while (token.IsCancellationRequested)
            {
                Console.Write("client> ");
                var query = Console.ReadLine();
                await writer.WriteAsync(query + "\n");
            }
        });
    }

    public static void Reader(NetworkStream stream, CancellationToken token)
    {
        Task.Run(async () =>
        {
            var reader = new StreamReader(stream);

            while (token.IsCancellationRequested)
            {
                var response = await reader.ReadAsync();

            }
        });
    }
}