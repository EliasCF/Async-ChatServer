using Microsoft.Extensions.DependencyInjection;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 7777; //Port to open server on

            IServiceCollection serviceProvider = new ServiceCollection()
                .AddScoped<ILogger, ConsoleLogger>()
                .AddScoped<MessageSender>(services => new MessageSender(services))
                .AddSingleton<ClientHandler>(services => new ClientHandler(services.GetService<ILogger>()))
                .AddSingleton<RoomHandler>(services => new RoomHandler(services.GetService<ILogger>()))
                .AddSingleton<ClientAcceptor>(services => new ClientAcceptor(services));

            Dispatcher dispatcher = new Dispatcher(serviceProvider, port);
            dispatcher.Dispatch();
        }
    }
}
