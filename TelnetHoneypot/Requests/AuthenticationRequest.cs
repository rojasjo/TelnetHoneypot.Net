using TelnetHoneypot.Services;

namespace TelnetHoneypot.Requests;

public class AuthenticationRequest
{
    private readonly IWriter _writer;

    public AuthenticationRequest(IWriter writer)
    {
        _writer = writer;
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

            if (username.Equals(defaultUsername) && password.Equals(defaultPassword))
            {
                _writer.Write(LogType.Message, "Authentication successful.\n");
                break;
            }

            _writer.Write(LogType.Message, "Invalid credentials. Please try again.\n");
        }
    }
}