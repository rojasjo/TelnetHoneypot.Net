namespace TelnetHoneypot.CVE;

public class Credential
{
    public string username { get; set; }

    public string password { get; set; }

    public string CVE { get; set; }


    public override string ToString()
    {
        return $"useraname: {username} - password: {password} - CVE: {CVE}";
    }
}