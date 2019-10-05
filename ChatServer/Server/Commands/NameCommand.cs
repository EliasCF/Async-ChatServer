using Microsoft.Extensions.DependencyInjection;

namespace ChatServer
{
    public class NameCommand : ICommand, IParameterCommand
    {
        public string parameter { get; }

        public string command { get; } = "/Name";

        private ClientHandler clients { get; }

        private MessageSender sender { get; }

        public NameCommand (ServiceProvider serivces, string param) 
        {
            clients = serivces.GetService<ClientHandler>();
            sender = serivces.GetService<MessageSender>();
            parameter = param;
        }

        public void handle(StateObject state) 
        {
            string clientName = parameter.Substring(command.Length + 1);

            //Set name of client if the name isn't taken
            if (!clients.Exists(clientName))
            {
                clients.SetName(state.client.id, clientName);
                clients.SetState(state.client.id, ClientState.Passive);
            }
            else 
            {
                string message = $"The name {clientName} is taken, pick a different name.\n\r";
                sender.Send(state.client, message);
            }
        }
    }
}