using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChatServer 
{
    public class JoinRoomCommand : ICommand, IParameterCommand
    {
        public string parameter { get; }

        public string command { get; } = "/RoomJoin";

        private ClientHandler clients { get; }
        
        private RoomHandler chatRooms { get; }

        public JoinRoomCommand (ServiceProvider services, string param)
        {
            clients = services.GetService<ClientHandler>();
            chatRooms = services.GetService<RoomHandler>();

            parameter = param;
        }

        public void handle(StateObject state) 
        {
            if (clients.Exists(state.client.id)) 
            {
                string roomName = parameter.Substring(command.Length + 1);

                Guid roomId = chatRooms.FindByName(roomName);
                clients.SetRoom(state.client.id, roomId);
            }
        }
    }
}