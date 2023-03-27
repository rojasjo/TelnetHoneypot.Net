using TelnetHoneypot.Core.Logging;

namespace TelnetHoneypot.Core.Commands;

public class HelpCommand : ICommand
{
    private readonly IWriter _writer;

    public HelpCommand(IWriter writer)
    {
        _writer = writer;
    }
    
    public void Execute(string message)
    {
        message = "Available commands:\n" +
                  "  help     Display this help text\n" +
                  "  pwd      Print working directory\n" +
                  "  dir      List files in current directory\n" +
                  "  cd       Change directory\n" +
                  "  date     Display current date and time\n" +
                  "  whoami   Display username\n" +
                  "  system   Display system information\n" +
                  "  ping     Ping a host\n" +
                  "  quit     Exit the Telnet session\n";
        
        _writer.Write(LogType.Response, message);
    }
}