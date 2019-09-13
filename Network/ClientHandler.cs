using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;
using System;

namespace ChatServer
{
    public class ClientHandler
    {
        private Logger logger = new Logger();

        private List<Client> clients = new List<Client>();

        /// <summary>
        /// Add a newly accepted client
        /// </summary>
        /// <param name="socket">The new socket connection</param>
        /// <param name="name">Name of the user</param>
        public Guid Add (Socket socket, string name) 
        {
            logger.Log($"Adding: '{socket.RemoteEndPoint.ToString()}' to client list");
            
            //Initialize new Client object
            Guid id = Guid.NewGuid();
            Client newClient = new Client(id, socket, name);

            clients.Add(newClient);

            return id;
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
        /// Get the last client in the list
        /// </summary>
        /// <returns>Newest client</returns>
        public Client GetLast () 
        {
            return clients.ElementAt(clients.Count());
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