using TelnetHoneypot.Core.Logging;

namespace TelnetHoneypot.Core.Requests;

public interface IAuthenticationService
{
    void Authenticate(IWriter writer, StreamReader streamReader);
}