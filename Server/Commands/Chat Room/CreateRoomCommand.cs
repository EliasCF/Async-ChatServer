namespace ChatServer
{
    public class CreateRoomCommand : ICommand
    {
        public string _message { get; }

        public string command { get; } = "/RoomCreate";

        public CreateRoomCommand () { }

        public CreateRoomCommand (string message)
        {
            _message = message;
        }

        public void handle(ref ClientHandler clients, ref RoomHandler chatRooms, StateObject state) 
        {
            chatRooms.Add(_message, state.client.id);
            clients.SetRoom(state.client.id, chatRooms.FindByName(_message));
        }
    }
}