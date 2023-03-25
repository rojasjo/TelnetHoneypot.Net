namespace TelnetHoneypot.CVE;

public interface IAuthenticationCVE
{
    bool AreKnownCredentials(string username, string password);
}