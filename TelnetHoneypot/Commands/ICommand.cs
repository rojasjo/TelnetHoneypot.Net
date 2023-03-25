using System.Net.Sockets;

namespace TelnetHoneypot.Commands;

public interface ICommand
{
    void Execute(string message);
}