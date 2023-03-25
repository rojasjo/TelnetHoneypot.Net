using System.Net.NetworkInformation;
using TelnetHoneypot.Services;

namespace TelnetHoneypot.Commands;

public class PingCommand : ICommand
{
    private readonly IWriter _writer;

    public PingCommand(IWriter writer)
    {
        _writer = writer;
    }

    public void Execute(string host)
    {
        try
        {
            var ping = new Ping();
            var reply = ping.Send(host);
            if (reply.Status == IPStatus.Success)
            {
                var pingTime = reply.RoundtripTime;
                _writer.Write(LogType.Response, "Pinging " + host + " with 32 bytes of data:\n");
                _writer.Write(LogType.Response, "Reply from " + host + ": time=" + pingTime + "ms\n");
            }
            else if (reply.Status == IPStatus.TimedOut)
            {
                _writer.Write(LogType.Response, "Ping request timed out.\n");
            }
            else
            {
                _writer.Write(LogType.Response, "Ping request failed.\n");
            }
        }
        catch (Exception e)
        {
            _writer.Write(LogType.Response, "Unable to ping host.\n");
        }
    }
}