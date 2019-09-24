using Microsoft.Extensions.DependencyInjection;

namespace ChatServer
{
    public class NameCommand : ICommand
    {
        public string _message { get; }

        public string command { get; } = "/Name";

        public ClientHandler clients { get; }

        public NameCommand () { }

        public NameCommand (ServiceProvider serivces, string message) 
        {
            clients = serivces.GetService<ClientHandler>();
            _message = message;
        }

        public void handle(StateObject state) 
        {
            clients.SetName(state.client.id, _message.Substring(6));
            clients.SetState(state.client.id, ClientState.Passive);
        }
    }
}