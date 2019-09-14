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
            string commandMessage = _message.Substring(0, _message.Length - 6);

            clients.SetName(state.client.id, commandMessage.Substring(6));
        }
    }
}