namespace TelnetHoneypot.Core.Logging;

public interface IWriter
{
    Stream Stream { set; }
    
    void Write(LogType logType, string message);

    void Write(byte[] buffer, int offset);
}