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

        private MessageSender sender { get; }

        public JoinRoomCommand (ServiceProvider services, string param)
        {
            clients = services.GetService<ClientHandler>();
            chatRooms = services.GetService<RoomHandler>();
            sender = services.GetService<MessageSender>();

            parameter = param;
        }

        public void handle(StateObject state) 
        {
            if (clients.Exists(state.client.id)) 
            {
                string roomName = parameter.Substring(command.Length + 1);

                Guid roomId = chatRooms.FindByName(roomName);
                clients.SetRoom(state.client.id, roomId);

                string message = $"{state.client.name} has joined room: {roomName}";
                sender.SendToAll(null, message, true);
            }
        }
    }
}