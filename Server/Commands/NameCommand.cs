namespace ChatServer
{
    public class NameCommand : ICommand
    {
        public string _message { get; }

        public string command { get; } = "/Name";

        public NameCommand () 
        { 

        }

        public NameCommand (string message) 
        {
            _message = message;
        }

        public void handle(ref ClientHandler clients, StateObject state) 
        {
            clients.SetName(state.client.id, _message.Substring(6));
            clients.SetState(state.client.id, ClientState.Passive);
        }
    }
}