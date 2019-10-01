using System;
using Microsoft.Extensions.DependencyInjection;

namespace ChatServer
{
    public class LeaveRoomCommand : ICommand
    {
        public string command { get; } = "/RoomDisc";

        private ClientHandler clients  { get; }

        private MessageSender sender { get; }

        public LeaveRoomCommand (ServiceProvider services) 
        {
            clients = services.GetService<ClientHandler>();
            sender = services.GetService<MessageSender>();
        }

        public void handle (StateObject state) 
        {
            string message = $"{state.client.name} left the room";

            sender.SendToAll(null, message, true);
            clients.SetRoom(state.client.id, Guid.Empty);
        }
    }
}