using System.Net.Sockets;

namespace TelnetHoneypot.Core.Telnet;

public interface IClientFactory
{
    IClient CreateClient(TcpClient tcpClient);
}