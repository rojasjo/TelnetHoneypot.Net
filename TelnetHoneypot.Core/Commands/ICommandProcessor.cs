using TelnetHoneypot.Core.Logging;

namespace TelnetHoneypot.Core.Commands;

public interface ICommandProcessor
{
    void Process(string command, IWriter writer);
}