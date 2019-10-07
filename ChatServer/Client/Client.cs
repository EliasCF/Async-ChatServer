using System.Net.Sockets;
using System.Linq;
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

        /// <summary>
        /// Guid for the client
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// Client's network connection
        /// </summary>
        public Socket connection { get; set; }
        
        /// <summary>
        /// Identifying name of the client
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// The state of the client
        /// </summary>
        public ClientState state { get; set; }

        /// <summary>
        /// The Guid of the room that the client is currently in.
        /// The Guid will be Guid.Empty if the client has not joined any room.
        /// </summary>
        public Guid roomId { get; set; }

        /// <summary>
        /// Indicate whether the client's current state allows running a specific command
        /// </summary>
        /// <param name="command">The command in question</param>
        /// <returns>Whether command is compatible with client state</returns>
        public bool AcceptsCommand (string command)
        {
            var incompatibilities = new IncompatibilityList().Incompatabilies;

            command = command.Split(' ')[0];

            if (incompatibilities.Where(c => c.Key == state && c.Value.Contains(command)).Count() > 0)
            {
                return false;
            }

            return true;
        }
    }
}