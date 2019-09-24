using System.Net.Sockets;
using System;

namespace ChatServer
{
    public class Client
    {
        public Client (Guid newId, Socket newConn, string newName, ClientState newState) 
        {
            id = newId;
            connection = newConn;
            name = newName;
            state = newState;
            roomId = Guid.Empty;
        }

        public Guid id { get; set; }

        public Socket connection { get; set; }
        
        public string name { get; set; }

        public ClientState state { get; set; }

        public Guid roomId { get; set; }
    }
}