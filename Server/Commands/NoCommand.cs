namespace ChatServer
{
    public class NoCommand : ICommand
    {
        public string command { get; } = "";

        public void handle(ref ClientHandler clients, StateObject state) { }
    }
}