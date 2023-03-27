using System.Text;

namespace TelnetHoneypot.Core.Logging;

public class Writer : IWriter
{
    public Stream Stream { private get; set; }
    
    public void Write(LogType logType, string message)
    {
        Logger.Log(logType, message);

        //don't rewrite the command in the console
        if (logType is LogType.Command or LogType.OnlyLog)
        {
            return;
        }

        var bytes = Encoding.ASCII.GetBytes(message);
        Stream.Write(bytes, 0, bytes.Length);
    }

    public void Write(byte[] buffer, int offset)
    {
        Stream.Write(buffer, offset, buffer.Length);
    }
}