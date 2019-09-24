using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 7777; //Port to open server on

            ServiceProvider serviceProvider = new ServiceCollection()
                .AddSingleton<ClientHandler>()
                .AddSingleton<RoomHandler>()
                .BuildServiceProvider();

            Dispatcher dispatcher = new Dispatcher(serviceProvider, port);
            dispatcher.Dispatch();
        }
    }
}
