using System.Net;
using System.Net.Sockets;
using TelnetHoneypot.Core.Services;

namespace TelnetHoneypot.Core.Telnet;

public class TcpListenerService : ITcpListenerService
{
    private readonly IClientFactory _clientFactory;

    private bool _listening = true;
    
    public TcpListenerService(IClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }
    
    public void Listen(int port)
    {
        var listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        
        Console.WriteLine("Telnet honeypot listening on port 23...");

        while (_listening)
        {
            var client = listener.AcceptTcpClient();
            var thread = new Thread(() => HandleClient(client));
            thread.Start();
        }
    }

    private void HandleClient(TcpClient tcpClient)
    {
        var client = _clientFactory.CreateClient(tcpClient);
        client.Init();
        client.Start();
        client.Close();
    }
    
    public void Stop()
    {
        _listening = false;
    }
}