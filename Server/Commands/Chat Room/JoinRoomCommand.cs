using System;

namespace ChatServer 
{
    public class JoinRoomCommand : ICommand
    {
        public string _message { get; }

        public string command { get; } = "/RoomJoin";

        public JoinRoomCommand () { }

        public JoinRoomCommand (string message)
        {
            _message = message;
        }

        public void handle(ref ClientHandler clients, ref RoomHandler chatRooms, StateObject state) 
        {
            if (clients.Exists(state.client.id)) 
            {
                Guid roomId = chatRooms.FindByName(_message);
                clients.SetRoom(state.client.id, roomId);
            }
        }
    }
}