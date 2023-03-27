using System.Globalization;
using TelnetHoneypot.Core.Logging;

namespace TelnetHoneypot.Core.Commands;

public class DateCommand : ICommand
{
    private readonly IWriter _writer;

    public DateCommand(IWriter writer)
    {
        _writer = writer;
    }

    public void Execute(string message)
    {
        var now = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        _writer.Write(LogType.Response,now + "\n");
    }
}