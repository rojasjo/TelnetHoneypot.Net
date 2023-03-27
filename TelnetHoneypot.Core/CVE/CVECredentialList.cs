namespace TelnetHoneypot.Core.CVE;

public static class CveCredentialList
{
    public static readonly List<Credential> KnownCredentials = new()
    {
        new Credential
        {
            Username = "dnsekakf2$$",
            Password = "",
            CVE = "CVE-2019-8950"
        },
        new Credential
        {
            Username = "admin",
            Password = "tlJwpbo6",
            CVE = "CVE-2022-45045"
        },
        new Credential
        {
            Username = "root",
            Password = "superzxmn",
            CVE = "CVE-2021-28152"
        },
        new Credential
        {
            Username = "gpon",
            Password = "gpon",
            CVE = "CVE-2021-27165"
        }
    };
}