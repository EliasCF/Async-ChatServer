using Microsoft.Extensions.DependencyInjection;

namespace ChatServer
{
    public class CreateRoomCommand : ICommand, IParameterCommand
    {
        public string parameter { get; }

        public string command { get; } = "/RoomCreate";

        private ClientHandler clients { get; }

        private RoomHandler chatRooms { get; }

        public CreateRoomCommand (ServiceProvider services, string param)
        {
            clients = services.GetService<ClientHandler>();
            chatRooms = services.GetService<RoomHandler>();
            parameter = param;
        }

        public void handle(StateObject state) 
        {
            string roomName = parameter.Substring(command.Length + 1);

            chatRooms.Add(roomName, state.client.id);
            clients.SetRoom(state.client.id, chatRooms.FindByName(roomName));
        }
    }
}