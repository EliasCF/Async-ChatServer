using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;
using System;

namespace ChatServer.Network
{
    public class ClientHandler
    {
        

        private List<Client> clients { get; set; }

        /// <summary>
        /// Add a newly accepted client
        /// </summary>
        /// <param name="newClient">The new socket connection</param>
        /// <param name="name">Name of the user</param>
        public void Add (Socket newClient, string name) 
        {
            Guid id = Guid.NewGuid();

            clients.Add(new Client(id, newClient, name));
        }

        /// <summary>
        /// Get a list of all connected client
        /// </summary>
        /// <returns>All connected clients</returns>
        public List<Client> GetAll () 
        {
            return clients;
        }

        /// <summary>
        /// Get a particular client by their id
        /// </summary>
        /// <param name="id">Client id</param>
        /// <returns>The specified client</returns>
        public Client GetId (Guid id) 
        {
            return clients.Single(c => c.id.Equals(id));
        }
    }
}