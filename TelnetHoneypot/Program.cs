using Microsoft.Extensions.DependencyInjection;
using TelnetHoneypot.Core.Commands;
using TelnetHoneypot.Core.CVE;
using TelnetHoneypot.Core.Logging;
using TelnetHoneypot.Core.Requests;
using TelnetHoneypot.Core.Services;
using TelnetHoneypot.Core.Telnet;

namespace TelnetHoneypot
{
    public class Program
    {
        private const int TelnetPort = 23;

        public static void Main(string[] args)
        {
            var provider = BuildServiceProvider();
            var tcpListenerService = provider.GetService<ITcpListenerService>();

            tcpListenerService?.Listen(TelnetPort);
        }

        private static ServiceProvider BuildServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<ITcpListenerService, TcpListenerService>();
            serviceCollection.AddSingleton<ICveService, CveService>();
            serviceCollection.AddSingleton<IClientFactory, ClientFactory>();
            serviceCollection.AddSingleton<ICommandProcessor, CommandProcessor>();
            serviceCollection.AddSingleton<IAuthenticationService, AuthenticationService>();

            serviceCollection.AddTransient<IWriter, Writer>();
            
            return serviceCollection.BuildServiceProvider();
        }
    }
}