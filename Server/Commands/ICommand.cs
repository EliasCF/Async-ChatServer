namespace ChatServer
{
    public interface ICommand
    {
         void handle(StateObject state);
         string command { get; }
    }
}