namespace TelnetHoneypot.Core.Services;

public interface ITcpListenerService
{
    void Listen(int port);

    void Stop();
}