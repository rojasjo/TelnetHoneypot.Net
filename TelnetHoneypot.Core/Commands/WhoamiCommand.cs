using TelnetHoneypot.Core.Logging;

namespace TelnetHoneypot.Core.Commands;

public class WhoamiCommand : ICommand
{
    private readonly IWriter _writer;

    public WhoamiCommand(IWriter writer)
    {
        _writer = writer;
    }

    public void Execute(string message)
    {
        var user = Environment.UserName;
        _writer.Write(LogType.Response, user + "\n");
    }
}