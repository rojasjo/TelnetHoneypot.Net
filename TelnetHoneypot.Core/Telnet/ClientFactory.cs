using System.Net.Sockets;
using Microsoft.Extensions.DependencyInjection;
using TelnetHoneypot.Core.Commands;
using TelnetHoneypot.Core.Logging;
using TelnetHoneypot.Core.Requests;

namespace TelnetHoneypot.Core.Telnet;

public class ClientFactory : IClientFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ClientFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IClient CreateClient(TcpClient tcpClient)
    {
        var writer = GetWriter();
        writer.Stream = tcpClient.GetStream();
        var client = new TelnetClient(GetAuthenticationService(), GetCommandProcessor(), writer)
        {
            Client = tcpClient
        };

        return client;
    }

    private IAuthenticationService GetAuthenticationService()
    {
        var authentication = _serviceProvider.GetService<IAuthenticationService>();

        if (authentication == null)
        {
            throw new Exception($"Implementation of {nameof(IAuthenticationService)} not registered");
        }

        return authentication;
    }

    private IWriter GetWriter()
    {
        var writer = _serviceProvider.GetService<IWriter>();
        if (writer == null)
        {
            throw new Exception($"Implementation of {nameof(IWriter)} not registered");
        }

        return writer;
    }

    private ICommandProcessor GetCommandProcessor()
    {
        var getCommandProcessor = _serviceProvider.GetService<ICommandProcessor>();
        if (getCommandProcessor == null)
        {
            throw new Exception($"Implementation of {nameof(ICommandProcessor)} not registered");
        }

        return getCommandProcessor;
    }
}