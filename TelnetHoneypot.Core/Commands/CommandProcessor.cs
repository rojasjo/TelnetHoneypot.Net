using System.Text.RegularExpressions;
using TelnetHoneypot.Core.Logging;

namespace TelnetHoneypot.Core.Commands;

public partial class CommandProcessor : ICommandProcessor
{

    [GeneratedRegex("^[a-zA-Z0-9\\s\\-\\._\\\\/:]+$")]
    private static partial Regex MyRegex();

    public void Process(string commandText, IWriter writer)
    {
        //sanitize the input, it could be malicious!
        var allowedPattern = MyRegex();
        if (!allowedPattern.IsMatch(commandText))
        {
            commandText = "Unrecognized";
        }

        ICommand command = null;
        var args = "";

        if (commandText.Equals("help", StringComparison.OrdinalIgnoreCase))
        {
            command = new HelpCommand(writer);
        }
        else if (commandText.Equals("pwd", StringComparison.OrdinalIgnoreCase))
        {
            command = new PwdCommand(writer);
        }
        else if (commandText.Equals("dir", StringComparison.OrdinalIgnoreCase))
        {
            command = new DirCommand(writer);
        }
        else if (commandText.StartsWith("cd ", StringComparison.OrdinalIgnoreCase))
        {
            command = new CdCommand(writer);
            args = commandText[3..];
        }
        else if (commandText.Equals("date", StringComparison.OrdinalIgnoreCase))
        {
            command = new DateCommand(writer);
        }
        else if (commandText.Equals("whoami", StringComparison.OrdinalIgnoreCase))
        {
            command = new WhoamiCommand(writer);
        }
        else if (commandText.Equals("system", StringComparison.OrdinalIgnoreCase))
        {
            command = new SystemCommand(writer);
        }
        else if (commandText.StartsWith("ping ", StringComparison.OrdinalIgnoreCase))
        {
            command = new PingCommand(writer);
            args = commandText[5..];
        }
        else
        {
            writer.Write(LogType.Response, "Unrecognized command: " + commandText + "\n");
        }

        command?.Execute(args);
    }
}