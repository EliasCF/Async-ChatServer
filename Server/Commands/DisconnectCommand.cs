namespace ChatServer.Server.Commands
{
    public class DisconnectCommand : ICommand
    {
        public string command { get; } = "/Disc";

        public DisconnectCommand () { }

        public void handle (ref ClientHandler clients, ref RoomHandler chatRooms, StateObject state) 
        {
            clients.Close(state.client.id);
        }
    }
}