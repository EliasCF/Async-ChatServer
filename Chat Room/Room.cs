using System;

namespace ChatServer
{
    public class Room
    {
        public Guid id { get; set; }

        public string name { get; set; }

        public Guid createdBy { get; set; }

        public DateTime creationTime { get; set; }
    }
}