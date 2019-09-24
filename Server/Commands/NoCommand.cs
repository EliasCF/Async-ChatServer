namespace ChatServer
{
    public class NoCommand : ICommand
    {
        public string command { get; } = "";

        public void handle(StateObject state) { }
    }
}