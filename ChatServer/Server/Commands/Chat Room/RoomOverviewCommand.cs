using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace ChatServer
{
    public class RoomOverviewCommand : ICommand
    {
        public string command { get; } = "/RoomOverview";

        private RoomHandler rooms { get; }

        private MessageSender sender { get; }

        public RoomOverviewCommand () { }

        public RoomOverviewCommand (ServiceProvider services)
        {
            rooms = services.GetService<RoomHandler>();
            sender = services.GetService<MessageSender>();
        }

        public void handle (StateObject state) 
        {
            IEnumerable<string> roomsList = rooms.GetAll().Select(room => room.name);
            string roomNames = $"=====\nRooms overview:\n{string.Join("\n", roomsList)}\n=====";

            sender.Send(state.client, roomNames);
        }
    }
}