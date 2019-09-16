namespace ChatServer
{
    public class CountCommand : ICommand
    {
        public string command { get; } = "/Count";

        public CountCommand () { }

        public void handle (ref ClientHandler clients, StateObject state) 
        {
            //Send message to clients, detailing the amount of active players
        }
    }
}