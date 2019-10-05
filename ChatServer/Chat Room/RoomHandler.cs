using System.Linq;
using System.Collections.Generic;
using System;

namespace ChatServer
{
    public class RoomHandler
    {
        private List<Room> chatRooms { get; set; }

        private ILogger logger { get; }

        public RoomHandler (ILogger log) 
        {
            chatRooms = new List<Room>();
            logger = log;
        }

        /// <summary>
        /// Current amount of chat rooms
        /// </summary>
        /// <value></value>
        public int Count 
        { 
            get 
            { 
                return chatRooms.Count; 
            } 
        }
        
        /// <summary>
        /// Add a room to the list of rooms
        /// </summary>
        /// <param name="name">Name of the new room</param>
        /// <param name="creator">Id of the client who has created the room</param>
        public Guid Add (string name, Guid creator)
        { 
            if (!chatRooms.Any(cr => cr.name == name)) 
            {
                logger.Log($"Creating new chat room: {name}");

                Guid id = Guid.NewGuid();

                chatRooms.Add(new Room 
                { 
                    id = id,
                    name = name,
                    createdBy = creator,
                    creationTime = DateTime.Now
                });

                return id;
            }

            return Guid.Empty;
        }

        /// <summary>
        /// Remove a room from the list of rooms
        /// </summary>
        /// <param name="id">Guid of room</param>
        public void Remove (Guid id) 
        {
            Room roomToRemove = chatRooms.SingleOrDefault(r => r.id == id);

            if (roomToRemove != null) {
                logger.Log($"Removing chat room: {roomToRemove.name}");
                chatRooms.Remove(roomToRemove);
            }
        }

        /// <summary>
        /// Get a list of all rooms
        /// </summary>
        /// <returns>All rooms</returns>
        public List<Room> GetAll ()
        {
            return chatRooms;
        }

        /// <summary>
        /// Get a rooms id by its name
        /// </summary>
        /// <param name="name">Name of room</param>
        /// <returns>Room id</returns>
        public Guid FindByName (string name)
        {
            if (name == null) return Guid.Empty;
            
            return chatRooms.SingleOrDefault(cr => cr.name == name).id;
        }
    }
}