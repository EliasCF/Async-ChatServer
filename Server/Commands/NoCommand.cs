namespace ChatServer
{
    public class NoCommand : ICommand
    {
        public void handle(ref ClientHandler clients, StateObject state) { }
    }
}