using Microsoft.Extensions.DependencyInjection;

namespace ChatServer
{
    public class ClientCountCommand : ICommand
    {
        public string command { get; } = "/ClientCount";

        public ClientCountCommand (ServiceProvider service) { }

        public void handle (StateObject state) 
        {
            //Send message to clients, detailing the amount of active players
        }
    }
}