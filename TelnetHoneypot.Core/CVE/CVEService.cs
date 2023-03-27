namespace TelnetHoneypot.Core.CVE;

public class CveService : ICveService
{
    public IEnumerable<Credential> CredentialsFromCve()
    {
        return CveCredentialList.KnownCredentials;
    }
}