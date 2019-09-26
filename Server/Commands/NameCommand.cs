using Microsoft.Extensions.DependencyInjection;

namespace ChatServer
{
    public class NameCommand : ICommand, IParameterCommand
    {
        public string parameter { get; }

        public string command { get; } = "/Name";

        public ClientHandler clients { get; }

        public NameCommand () { }

        public NameCommand (ServiceProvider serivces, string param) 
        {
            clients = serivces.GetService<ClientHandler>();
            parameter = param;
        }

        public void handle(StateObject state) 
        {
            string clientName = parameter.Substring(command.Length + 1);

            clients.SetName(state.client.id, clientName);
            clients.SetState(state.client.id, ClientState.Passive);
        }
    }
}