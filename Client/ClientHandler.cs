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

            logger.Log("");
            clients[index].state = newState; 
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
    }
}