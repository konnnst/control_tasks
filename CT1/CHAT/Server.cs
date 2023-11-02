namespace Chat;

/// <summary>
/// Runs server
/// </summary>
public class Server
{
    private CancellationToken _cancellationToken;
    private int _port;

    public Server(int port)
    {
        _port = port;
    }

    public void Start()
    {
        var client = new TcpClient(_IP.ToString(), _port);
        var clientStream = client.GetStream();

        TcpIO.Writer(clientStream, _cancellationToken);
        TcpIO.Reader(clientStream, _cancellationToken);

        while (!_cancellationToken.IsCancellationRequested)
        {
        }
    }
}