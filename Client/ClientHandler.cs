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
            Client newClient = new Client(id, socket, name, ClientState.NeedName);

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
        /// Get a particular client by their id
        /// </summary>
        /// <param name="id">Client id</param>
        /// <returns>The specified client</returns>
        public Client GetId (Guid id) 
        {
            return clients.Single(c => c.id.Equals(id));
        }

        /// <summary>
        /// Sets the name of a specific client
        /// </summary>
        /// <param name="id">The client's Guid identification</param>
        /// <param name="name">The name to give the client</param>
        public void SetName (Guid id, string name) 
        {
            int index = clients.FindIndex(c => c.id == id);

            logger.Log($"Setting name of client: '{clients[index].connection.RemoteEndPoint.ToString()}', to: '{name}',");
            clients[index].name = name;
        }

        /// <summary>
        /// Change the state of a client
        /// </summary>
        /// <param name="id">the client's Guid identification</param>
        /// <param name="newState">The state to change the current state to</param>
        public void SetState (Guid id, ClientState newState)
        {
            int index = clients.FindIndex(c => c.id == id);

            if (index != -1) 
            {
                clients[index].state = newState; 
            }
        }

        /// <summary>
        /// Change the room that a client is connected to
        /// </summary>
        /// <param name="id">The client's Guid identification</param>
        /// <param name="room">The room's Guid identification</param>
        public void SetRoom (Guid id, Guid room) 
        {
            int index = clients.FindIndex(c => c.id == id);

            if (index != -1) 
            {
                clients[index].roomId = room;
            }

        }

        /// <summary>
        /// Close a specific client
        /// </summary>
        /// <param name="id">The client's Guid identification</param>
        public void Close (Guid id) 
        {
            int index = clients.FindIndex(c => c.id == id);

            logger.Log($"Closing client: {clients[index].connection.RemoteEndPoint.ToString()}");
            clients[index].connection.Close();
            clients.RemoveAt(index);
        }

        /// <summary>
        /// Check if a specific Client exists
        /// </summary>
        /// <param name="id">The client's Guid identification</param>
        /// <returns>Does the client exist</returns>
        public bool Exists (Guid id) 
        {
            return clients.SingleOrDefault(c => c.id.Equals(id)) == null ? false : true;
        }
    }
}