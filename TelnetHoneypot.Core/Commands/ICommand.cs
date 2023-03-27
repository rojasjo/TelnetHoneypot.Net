namespace TelnetHoneypot.Core.Commands;

public interface ICommand
{
    void Execute(string message);
}