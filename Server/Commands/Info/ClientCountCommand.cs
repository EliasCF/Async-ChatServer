using Microsoft.Extensions.DependencyInjection;

namespace ChatServer
{
    public class ClientCountCommand : ICommand
    {
        public string command { get; } = "/ClientCount";

        private MessageSender sender  { get; }

        private ClientHandler clients { get; }

        public ClientCountCommand (ServiceProvider service) 
        {
            sender = service.GetService<MessageSender>();
            clients = service.GetService<ClientHandler>();
        }

        public void handle (StateObject state) 
        {
            string message =  $"Current amount of server clients: {clients.Count}";

            sender.SendToAll(null, message, true);
        }
    }
}