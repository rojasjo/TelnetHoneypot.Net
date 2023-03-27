namespace TelnetHoneypot.Core.CVE;

public class Credential
{
    public string Username { get; set; }

    public string Password { get; set; }

    public string CVE { get; set; }


    public override string ToString()
    {
        return $"useraname: {Username} - password: {Password} - CVE: {CVE}";
    }
}