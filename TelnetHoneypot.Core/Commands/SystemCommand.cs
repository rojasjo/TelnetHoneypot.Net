using TelnetHoneypot.Core.Logging;

namespace TelnetHoneypot.Core.Commands;

public class SystemCommand : ICommand
{
    private readonly IWriter _writer;

    public SystemCommand(IWriter writer)
    {
        _writer = writer;
    }

    public void Execute(string message)
    {
        var osName = Environment.OSVersion.VersionString;
        _writer.Write(LogType.Response, "OS: " + osName + "\n");
    }
}