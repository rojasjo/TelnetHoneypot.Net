using System.Net.Sockets;
using System.Text;

namespace TelnetHoneypot.Services;

public class Writer : IWriter
{
    private readonly NetworkStream _networkStream;

    public Stream Stream => _networkStream;
    
    public Writer(NetworkStream networkStream)
    {
        _networkStream = networkStream;
    }
    
    public void Write(LogType logType, string message)
    {
        Logger.Log(logType, message);

        //don't rewrite the command in the console
        if (logType == LogType.Command)
        {
            return;
        }
        
        var bytes = Encoding.ASCII.GetBytes(message);
        _networkStream.Write(bytes, 0, bytes.Length);
    }

    public void Write(byte[] buffer, int offset)
    {
        _networkStream.Write(buffer, offset, buffer.Length);
    }
}