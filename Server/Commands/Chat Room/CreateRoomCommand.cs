using Microsoft.Extensions.DependencyInjection;

namespace ChatServer
{
    public class CreateRoomCommand : ICommand
    {
        public string _message { get; }

        public string command { get; } = "/RoomCreate";

        public ClientHandler clients { get; }

        public RoomHandler chatRooms { get; }

        public CreateRoomCommand () { }

        public CreateRoomCommand (ServiceProvider services, string message)
        {
            clients = services.GetService<ClientHandler>();
            chatRooms = services.GetService<RoomHandler>();
            _message = message;
        }

        public void handle(StateObject state) 
        {
            string roomName = _message.Substring(command.Length + 1);

            chatRooms.Add(roomName, state.client.id);
            clients.SetRoom(state.client.id, chatRooms.FindByName(roomName));
        }
    }
}