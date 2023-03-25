using TelnetHoneypot.Services;

namespace TelnetHoneypot.Commands;

public class PwdCommand : ICommand
{
    private readonly IWriter _writer;

    public PwdCommand(IWriter writer)
    {
        _writer = writer;
    }

    public void Execute(string message)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        _writer.Write(LogType.Response, currentDirectory + "\n");
    }
}