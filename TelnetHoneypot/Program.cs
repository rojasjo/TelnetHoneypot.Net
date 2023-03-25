using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using TelnetHoneypot.Commands;
using TelnetHoneypot.CVE;
using TelnetHoneypot.Requests;
using TelnetHoneypot.Services;

namespace TelnetHoneypot
{
    public class Program
    {
        private const int TelnetPort = 23;

        static void Main(string[] args)
        {
            StartTelnetHoneypot();
        }

        private static void StartTelnetHoneypot()
        {
            var listener = new TcpListener(IPAddress.Any, TelnetPort);
            listener.Start();
            Console.WriteLine("Telnet honeypot listening on port 23...");

            while (true)
            {
                var client = listener.AcceptTcpClient();
                var thread = new Thread(() => HandleClient(client));
                thread.Start();
            }
        }

        private static void HandleClient(TcpClient client)
        {
            using var stream = client.GetStream();
            var writer = new Writer(stream);
            SetupTelnetNegotiation(writer);
            SendWelcomeMessage(writer);
            ProcessCommands(writer);

            client.Close();
        }

        private static void SetupTelnetNegotiation(IWriter writer)
        {
            // Set up Telnet negotiation
            var options = new TelnetOptions(writer);
            options.Negotiate();
        }

        private static void SendWelcomeMessage(IWriter writer)
        {
            writer.Write(LogType.Message, "Welcome to the Telnet honeypot. Type 'help' for a list of commands.\n");
        }

        private static void ProcessCommands(IWriter writer)
        {
            using (var reader = new StreamReader(writer.Stream, Encoding.ASCII))
            using (var swriter = new StreamWriter(writer.Stream, Encoding.ASCII))
            {
                swriter.AutoFlush = true;
                var authenticationCve = new AuthenticationCVE();
                var authentication = new AuthenticationRequest(writer, authenticationCve);
                authentication.Authenticate(reader);

                while (true)
                {
                    writer.Write(LogType.None, "$ ");
                    var command = reader.ReadLine()?.Trim().ToLower();

                    writer.Write(LogType.Command, command);

                    if (command.Equals("quit", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }

                    ProcessCommand(command, writer);
                }
            }
        }

        private static void ProcessCommand(string commandText, IWriter writer)
        {
            //sanitize the input, it could be malicious!
            var allowedPattern = new Regex(@"^[a-zA-Z0-9\s\-\._\\/:]+$");
            if (!allowedPattern.IsMatch(commandText))
            {
                commandText = "Unrecognized";
            }

            ICommand command = null;
            var args = "";

            if (commandText.Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                command = new HelpCommand(writer);
            }
            else if (commandText.Equals("pwd", StringComparison.OrdinalIgnoreCase))
            {
                command = new PwdCommand(writer);
            }
            else if (commandText.Equals("dir", StringComparison.OrdinalIgnoreCase))
            {
                command = new DirCommand(writer);
            }
            else if (commandText.StartsWith("cd ", StringComparison.OrdinalIgnoreCase))
            {
                command = new CdCommand(writer);
                args = commandText.Substring(3);
            }
            else if (commandText.Equals("date", StringComparison.OrdinalIgnoreCase))
            {
                command = new DateCommand(writer);
            }
            else if (commandText.Equals("whoami", StringComparison.OrdinalIgnoreCase))
            {
                command = new WhoamiCommand(writer);
            }
            else if (commandText.Equals("system", StringComparison.OrdinalIgnoreCase))
            {
                command = new SystemCommand(writer);
            }
            else if (commandText.StartsWith("ping ", StringComparison.OrdinalIgnoreCase))
            {
                command = new PingCommand(writer);
                args = commandText.Substring(5);
            }
            else
            {
                writer.Write(LogType.Response, "Unrecognized command: " + commandText + "\n");
            }

            command?.Execute(args);
        }
    }
}