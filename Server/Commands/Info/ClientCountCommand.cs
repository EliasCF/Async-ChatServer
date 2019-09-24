namespace ChatServer
{
    public class ClientCountCommand : ICommand
    {
        public string command { get; } = "/ClientCount";

        public ClientCountCommand () { }

        public void handle (ref ClientHandler clients, ref RoomHandler chatRooms, StateObject state) 
        {
            //Send message to clients, detailing the amount of active players
        }
    }
}