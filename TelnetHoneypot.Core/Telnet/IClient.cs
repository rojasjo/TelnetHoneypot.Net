namespace TelnetHoneypot.Core.Telnet;

public interface IClient
{
    void Init();
    
    void Start();

    void Close();
}