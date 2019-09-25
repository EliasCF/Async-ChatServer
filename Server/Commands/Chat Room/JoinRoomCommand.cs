using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChatServer 
{
    public class JoinRoomCommand : ICommand
    {
        public string _message { get; }

        public string command { get; } = "/RoomJoin";

        public ClientHandler clients { get; }
        
        public RoomHandler chatRooms { get; }

        public JoinRoomCommand () { }

        public JoinRoomCommand (ServiceProvider services, string message)
        {
            clients = services.GetService<ClientHandler>();
            chatRooms = services.GetService<RoomHandler>();

            _message = message;
        }

        public void handle(StateObject state) 
        {
            if (clients.Exists(state.client.id)) 
            {
                string roomName = _message.Substring(command.Length + 1);

                Guid roomId = chatRooms.FindByName(roomName);
                clients.SetRoom(state.client.id, roomId);
            }
        }
    }
}