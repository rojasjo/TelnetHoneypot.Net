using TelnetHoneypot.Core.Logging;

namespace TelnetHoneypot.Core.Commands;

public class CdCommand : ICommand
{
    private readonly IWriter _writer;

    public CdCommand(IWriter writer)
    {
        _writer = writer;
    }

    public void Execute(string path)
    {
        try
        {
            Directory.SetCurrentDirectory(path);
        }
        catch (Exception e)
        {
            _writer.Write(LogType.Response,e.Message + "\n");
        }
    }
}