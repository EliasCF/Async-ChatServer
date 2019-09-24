using System.Linq;
using System.Collections.Generic;
using System;

namespace ChatServer
{
    public class RoomHandler
    {
        private List<Room> chatRooms = new List<Room>();

        private Logger logger = new Logger();
        
        public void Add (string name, Guid creator)
        { 
            if (!chatRooms.Any(cr => cr.name == name)) 
            {
                logger.Log($"Creating new chat room: {name}");

                chatRooms.Add(new Room 
                { 
                    id = Guid.NewGuid(),
                    name = name,
                    createdBy = creator,
                    creationTime = DateTime.Now
                });
            }
        }

        public void Remove (Guid id) 
        {
            Room roomToRemove = chatRooms.SingleOrDefault(r => r.id == id);

            if (roomToRemove != null) {
                logger.Log($"Removing chat room: {roomToRemove.name}");
                chatRooms.Remove(roomToRemove);
            }
        }

        public Guid FindByName (string name)
        {
            if (name == null) return Guid.Empty;
            
            return chatRooms.SingleOrDefault(cr => cr.name == name).id;
        }
    }
}