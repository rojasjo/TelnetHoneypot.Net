using TelnetHoneypot;
using TelnetHoneypot.Commands;
using TelnetHoneypot.Services;

class TelnetOptions
{
    private readonly IWriter _writer;

    public TelnetOptions(IWriter writer)
    {
        _writer = writer;
    }
    
    public void Negotiate()
    {
        SendCommand(TelnetCommand.IAC, TelnetCommand.WILL, TelnetOption.ECHO);
        SendCommand(TelnetCommand.IAC, TelnetCommand.WILL, TelnetOption.SGA);
    }
    
    private void SendCommand(TelnetCommand command, TelnetCommand subCommand, TelnetOption option)
    {
        var buffer = new byte[] { (byte)TelnetCommand.IAC, (byte)command, (byte)subCommand, (byte)option };
        _writer.Write(buffer, 0);
    }
}