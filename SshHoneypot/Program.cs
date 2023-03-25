using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SshHoneypot;

public class Program
{
    public static void Main(string[] args)
    {
        if (!int.TryParse(args[0], out var port) || port > 65535)
        {
            Console.WriteLine($"Invalid port {args[0]}, it must be a number between 0 and 65535");
            return;
        }

        // Create a TCP listener on the chosen port
        var listener = new TcpListener(IPAddress.Any, port);
        listener.Start();

        Console.WriteLine("Listening on port " + port);

        while (true)
        {
            // Accept incoming client connections
            var client = listener.AcceptTcpClient();

            // Get the IP address of the client
            var clientIp = ((IPEndPoint)client.Client.RemoteEndPoint!).Address;

            // Log information about the incoming request
            Console.WriteLine("Incoming connection from " + clientIp + " at " + DateTime.Now);

            // Send a fake response to the client
            const string response =
                "HTTP/1.1 200 OK\r\nContent-Type: text/html\r\n\r\n<html><body><h1>Welcome to the honeypot!</h1></body></html>";
            var bytes = Encoding.ASCII.GetBytes(response);
            try
            {
                client.GetStream().Write(bytes, 0, bytes.Length);
            }
            catch (Exception e)
            {
                //connection closed by the client before getting the response
                Console.WriteLine("Probably nmap scan from " + clientIp + " at " + DateTime.Now);
            }
            
            client.Close();
            Console.WriteLine("Closed connection from " + clientIp + " at " + DateTime.Now);
        }
    }
}