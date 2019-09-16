namespace ChatServer
{
    public class NameCommand : ICommand
    {
        private string _message { get; }

        public NameCommand (string message) 
        {
            _message = message;
        }

        public void handle(ref ClientHandler clients, StateObject state) 
        {
            clients.SetName(state.client.id, _message.Substring(6));
        }
    }
}