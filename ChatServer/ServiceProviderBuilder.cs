using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChatServer
{
    public class ServiceProviderBuilder
    {
        private IServiceCollection collection { get; }
        
        public ServiceProviderBuilder ()
        {
            collection = new ServiceCollection()
               .AddScoped<ILogger, ConsoleLogger>()
               .AddScoped<MessageSender>(services => new MessageSender(services))
               .AddSingleton<ClientHandler>(services => new ClientHandler(services.GetService<ILogger>()))
               .AddSingleton<RoomHandler>(services => new RoomHandler(services.GetService<ILogger>()))
               .AddSingleton<ClientAcceptor>(services => new ClientAcceptor(services));
        }

        public IServiceProvider Build ()
        {
            return collection.BuildServiceProvider();
        }

        public IServiceCollection Get ()
        {
            return collection;
        }
    }
}