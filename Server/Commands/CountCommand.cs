namespace ChatServer
{
    public class CountCommand : ICommand
    {
        public void handle (ref ClientHandler clients, StateObject state) 
        {
            //Send message to clients, detailing the amount of active players
        }
    }
}