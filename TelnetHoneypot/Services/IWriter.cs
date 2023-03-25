namespace TelnetHoneypot.Services;

public interface IWriter
{
    Stream Stream { get; }
    
    void Write(LogType logType, string message);

    void Write(byte[] buffer, int offset);
}