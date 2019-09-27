using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 7777; //Port to open server on

            IServiceCollection serviceProvider = new ServiceCollection()
                .AddSingleton<ClientHandler>()
                .AddSingleton<RoomHandler>()
                .AddScoped<Logger>()
                .AddScoped<MessageSender>(services => new MessageSender(services));

            Dispatcher dispatcher = new Dispatcher(serviceProvider, port);
            dispatcher.Dispatch();
        }
    }
}
