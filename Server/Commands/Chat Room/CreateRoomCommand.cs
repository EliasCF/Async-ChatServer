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
            chatRooms.Add(_message, state.client.id);
            clients.SetRoom(state.client.id, chatRooms.FindByName(_message));
        }
    }
}