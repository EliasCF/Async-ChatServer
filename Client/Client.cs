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
        }

        public Guid id;

        public Socket connection;
        
        public string name;

        public ClientState state;
    }
}