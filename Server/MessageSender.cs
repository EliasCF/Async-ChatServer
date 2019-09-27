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

        public void SendToAll (Client from, string message, bool excludeSender) 
        {
            List<Client> SendTo = clients.GetAll();

            if (excludeSender) SendTo = SendTo.Where(c => c.id != from.id && c.roomId == from.roomId).ToList();

            foreach (Client client in SendTo) 
            {
                Send(client, $"{from.name}: {message}\r\n");
            }
        }

        public void SendCallback (IAsyncResult result) 
        {
            try 
            {
                Client client = (Client)result.AsyncState;

                int byteSent = client.connection.EndSend(result);
                logger.Log($"Sent ");
            }
            catch (Exception e) 
            {
                logger.Log(e.ToString());
            }
        }
    }
}