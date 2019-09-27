using Microsoft.Extensions.DependencyInjection;

namespace ChatServer.Server.Commands
{
    public class DisconnectCommand : ICommand
    {
        public string command { get; } = "/Disc";

        private ClientHandler clients { get; }

        private MessageSender sender { get; }

        public DisconnectCommand (ServiceProvider service) 
        { 
            clients = service.GetService<ClientHandler>();
            sender = service.GetService<MessageSender>();
        }

        public void handle (StateObject state) 
        {
            string message =  $"{state.client.name} left the chat";

            clients.Close(state.client.id);
            sender.SendToAll(null, message, true);
        }
    }
}