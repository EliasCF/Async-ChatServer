using Microsoft.Extensions.DependencyInjection;

namespace ChatServer.Server.Commands
{
    public class DisconnectCommand : ICommand
    {
        public string command { get; } = "/Disc";

        public ClientHandler clients { get; }

        public DisconnectCommand (ServiceProvider service) 
        { 
            clients = service.GetService<ClientHandler>();
        }

        public void handle (StateObject state) 
        {
            clients.Close(state.client.id);
        }
    }
}