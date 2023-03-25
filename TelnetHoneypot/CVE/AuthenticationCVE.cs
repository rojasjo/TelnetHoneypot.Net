using TelnetHoneypot.Services;

namespace TelnetHoneypot.CVE;

public class AuthenticationCVE : IAuthenticationCVE
{
    private readonly List<Credential> _knownCredentials = new()
    {
        new Credential
        {
            username = "dnsekakf2$$",
            password = "",
            CVE = "CVE-2019-8950"
        },
        new Credential
        {
            username = "admin",
            password = "tlJwpbo6",
            CVE = "CVE-2022-45045"
        },
        new Credential
        {
            username = "root",
            password = "superzxmn",
            CVE = "CVE-2021-28152"
        },
        new Credential
        {
            username = "gpon",
            password = "gpon",
            CVE = "CVE-2021-27165"
        }
    };

    public bool AreKnownCredentials(string username, string password)
    {
        var knownCredential = _knownCredentials.FirstOrDefault(c => c.username == username && c.password == password);

        if (knownCredential == null)
        {
            return false;
        }

        Logger.Log(LogType.Message, $"#CVE {knownCredential}");
        return true;
    }
}