namespace TelnetHoneypot.Core.CVE;

public interface ICveService
{
    IEnumerable<Credential> CredentialsFromCve();
}