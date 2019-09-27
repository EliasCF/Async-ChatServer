using System;

namespace ChatServer
{
    public class Room
    {   
        /// <summary>
        /// Guid for the room.
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// Name of the room.
        /// Will be used by user for identifying the room.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Guid of the client that created the room
        /// </summary>
        public Guid createdBy { get; set; }

        /// <summary>
        /// The time the room was created
        /// </summary>
        public DateTime creationTime { get; set; }
    }
}