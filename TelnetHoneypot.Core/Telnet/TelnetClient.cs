using System.Net;
using System.Net.Sockets;
using System.Text;
using TelnetHoneypot.Core.Commands;
using TelnetHoneypot.Core.Logging;
using TelnetHoneypot.Core.Requests;

namespace TelnetHoneypot.Core.Telnet;

public class TelnetClient : IClient
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ICommandProcessor _commandProcessor;
    private readonly IWriter _writer;

    public TcpClient Client { init; private get; } = null!;

    public TelnetClient(IAuthenticationService authenticationService, ICommandProcessor commandProcessor,
        IWriter writer)
    {
        _authenticationService = authenticationService;
        _commandProcessor = commandProcessor;
        _writer = writer;
    }

    public void Init()
    {
        _writer.Write(LogType.Message, "Welcome to the MegaBank. Type 'help' for a list of commands.\n");

        if (Client.Client.RemoteEndPoint is not IPEndPoint remoteEndPoint)
        {
            return;
        }
        
        var remoteAddress = remoteEndPoint.Address.ToString();
        var remotePort = remoteEndPoint.Port;
        
        _writer.Write(LogType.OnlyLog, $"Incoming connection from {remoteAddress}:{remotePort}");
    }

    public void Start()
    {
        var clientStream = Client.GetStream();
        using (var streamReader = new StreamReader(clientStream, Encoding.ASCII))
        using (var streamWriter = new StreamWriter(clientStream, Encoding.ASCII))
        {
            streamWriter.AutoFlush = true;

            _authenticationService.Authenticate(_writer, streamReader);

            while (true)
            {
                _writer.Write(LogType.None, "$ ");
                var command = streamReader.ReadLine()?.Trim().ToLower() ?? string.Empty;

                _writer.Write(LogType.Command, command);

                if (command.Equals("quit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                _commandProcessor.Process(command, _writer);
            }
        }
    }

    public void Close()
    {
        Client.Close();
    }
}