using TelnetHoneypot.Core.CVE;
using TelnetHoneypot.Core.Logging;

namespace TelnetHoneypot.Core.Requests;

public class AuthenticationService : IAuthenticationService
{
    private readonly ICveService _cveService;

    private List<Credential> _credentialsFromCve;

    private const string DefaultUsername = "admin";
    private const string DefaultPassword = "admin";

    public AuthenticationService(ICveService cveService)
    {
        _cveService = cveService;
    }

    public void Authenticate(IWriter writer, StreamReader streamReader)
    {
        _credentialsFromCve = _cveService.CredentialsFromCve().ToList();

        while (true)
        {
            writer.Write(LogType.Message, "Username: ");
            var username = streamReader.ReadLine() ?? string.Empty;

            writer.Write(LogType.Message, "Password: ");
            var password = streamReader.ReadLine() ?? string.Empty;

            var validLogin = username.Equals(DefaultUsername) && password.Equals(DefaultPassword);
            validLogin |= AreKnownCredentials(username, password);

            if (validLogin)
            {
                writer.Write(LogType.Message, "Authentication successful.\n");
                break;
            }

            writer.Write(LogType.Message, "Invalid credentials. Please try again.\n");
        }

        bool AreKnownCredentials(string username, string password)
        {
            var knownCredential =
                _credentialsFromCve.FirstOrDefault(c => c.Username == username && c.Password == password);

            if (knownCredential == null)
            {
                return false;
            }

            writer.Write(LogType.Message, $"#CVE {knownCredential}");
            return true;
        }
    }
}