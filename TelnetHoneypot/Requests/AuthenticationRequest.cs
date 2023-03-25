using TelnetHoneypot.CVE;
using TelnetHoneypot.Services;

namespace TelnetHoneypot.Requests;

public class AuthenticationRequest
{
    private readonly IWriter _writer;
    private readonly IAuthenticationCVE _authenticationCve;

    public AuthenticationRequest(IWriter writer, IAuthenticationCVE authenticationCve)
    {
        _writer = writer;
        _authenticationCve = authenticationCve;
    }

    public void Authenticate(StreamReader reader)
    {
        const string defaultUsername = "admin";
        const string defaultPassword = "admin";

        while (true)
        {
            _writer.Write(LogType.Message, "Username: ");
            var username = reader.ReadLine();

            _writer.Write(LogType.Message, "Password: ");
            var password = reader.ReadLine();

            var defaultCredentials = username.Equals(defaultUsername) && password.Equals(defaultPassword);
            var knownCredential = _authenticationCve.AreKnownCredentials(username, password);
            
            if (defaultCredentials || knownCredential)
            {
                _writer.Write(LogType.Message, "Authentication successful.\n");
                break;
            }

            _writer.Write(LogType.Message, "Invalid credentials. Please try again.\n");
        }
    }
}