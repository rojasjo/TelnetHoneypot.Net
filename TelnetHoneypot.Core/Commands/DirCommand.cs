using System.Text;
using TelnetHoneypot.Core.Logging;

namespace TelnetHoneypot.Core.Commands;

public class DirCommand : ICommand
{
    private readonly IWriter _writer;

    public DirCommand(IWriter writer)
    {
        _writer = writer;
    }

    public void Execute(string message)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var files = Directory.GetFiles(currentDirectory);
        var sb = new StringBuilder();
        foreach (var file in files)
        {
            sb.AppendLine(file);
        }
        
        _writer.Write(LogType.Response,  sb.ToString());
    }
}