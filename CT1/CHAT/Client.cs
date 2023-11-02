namespace Chat;

/// <summary>
/// Runs client
/// </summary>
public class Client
{
    private CancellationToken _cancellationToken;
    private int _port;
    private IPAddress _IP;

    public Client(IPAddress IP, int port)
    {
        _IP = IP;
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