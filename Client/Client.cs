using System.Net.Sockets;
using System;

namespace ChatServer
{
    public class Client
    {
        public Client (Guid newId, Socket newConn, string newName) 
        {
            id = newId;
            connection = newConn;
            name = newName;
        }

        public Guid id;

        public Socket connection;
        
        public string name;

        public ClientState state;
    }
}