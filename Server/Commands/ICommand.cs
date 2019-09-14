namespace ChatServer
{
    public interface ICommand
    {
         void handle(ref ClientHandler clients, StateObject state);
    }
}