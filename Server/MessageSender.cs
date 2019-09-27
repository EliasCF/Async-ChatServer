using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace ChatServer
{
    public class MessageSender
    {
        private Logger logger { get; }
        public ClientHandler clients { get; }

        public MessageSender (IServiceProvider services)
        {
            logger = services.GetService<Logger>();
            clients = services.GetService<ClientHandler>();
        }

        /// <summary>
        /// Sends a text message to a specific client
        /// </summary>
        /// <param name="client">Client object of the client to receive the message</param>
        /// <param name="data"></param>
        public void Send (Client client, string data) 
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            logger.Log($"Sending message: '{data}' to '{client.name}'");
            
            client.connection.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        /// <summary>
        /// Send a text message to all connected clients
        /// </summary>
        /// <param name="from">The client who sent the message</param>
        /// <param name="message">Message to be sent</param>
        /// <param name="excludeSender">Whether or not the message should be sent back to the sender</param>
        public void SendToAll (Client from, string message, bool excludeSender) 
        {
            List<Client> SendTo = clients.GetAll();

            string text = $"{message}\r\n";

            if (from != null) 
            {
                if (excludeSender) 
                {
                    SendTo = SendTo.Where(c => c.id != from.id && c.roomId == from.roomId).ToList();
                }
                
                text = $"{from.name}: " + text;
            }

            foreach (Client client in SendTo) 
            {
                Send(client, text);
            }
        }

        /// <summary>
        /// Async callback for send method
        /// </summary>
        /// <param name="result">Client object of the receiving client</param>
        public void SendCallback (IAsyncResult result) 
        {
            try 
            {
                Client client = (Client)result.AsyncState;

                int byteSent = client.connection.EndSend(result);
            }
            catch (Exception e) 
            {
                logger.Log(e.ToString());
            }
        }
    }
}