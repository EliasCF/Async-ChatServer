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
                Guid roomId = chatRooms.FindByName(_message);
                clients.SetRoom(state.client.id, roomId);
            }
        }
    }
}