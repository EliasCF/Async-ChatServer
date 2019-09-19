using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
using System.Text;
using System;

namespace ChatServer
{
    public class ServerIO
    {
        private ClientHandler clients = new ClientHandler(); //Connected clients
        ManualResetEvent allDone { get; set; }
        private Logger logger = new Logger();

        public ServerIO (ref ManualResetEvent resetEvent) 
        {
            allDone = resetEvent;
        }

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

            if (excludeSender) SendTo = SendTo.Where(c => c.id != from.id).ToList();

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

        /// <summary>
        /// Accept client and receive their first message
        /// </summary>
        /// <param name="result">Socket of the accepted client</param>
        public void AcceptCallback (IAsyncResult result) 
        {
            Socket listener = (Socket)result.AsyncState;
            Socket handler = listener.EndAccept(result);

            logger.Log($"Accepting connection from: '{handler.RemoteEndPoint.ToString()}'");

            Guid newClientId = clients.Add(handler, string.Empty);
            StateObject state = new StateObject(clients.GetId(newClientId));

            Send(state.client, "Welcome, you need to set your name by typing the name command: '/Name <name>' \r\nExample: '/Name Lars'\r\n");

            allDone.Set();

            state.client.connection
                .BeginReceive(state.buffer, 0, StateObject.bufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
        }

        /// <summary>
        /// Read message from a client
        /// </summary>
        /// <param name="result">StateObject containing the client that is being read from</param>
        public void ReadCallback (IAsyncResult result) 
        {      
            StateObject state = (StateObject)result.AsyncState;

            logger.Log($"Reading message from: '{state.client.connection.RemoteEndPoint.ToString()}'");

            int byteRead = state.client.connection.EndReceive(result);

            if (byteRead > 0) 
            {
                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, byteRead));
                string content = state.sb.ToString();

                if (content.IndexOf("<EOF>") > -1)
                {
                    logger.Log($"Read {content.Length} bytes from socket. \nData: '{content.Substring(0, content.Length - 6)}'");

                    string message = content.Substring(0, content.Length - 6);

                    CommandFactory factory = new CommandFactory();
                    ICommand command = factory.Build(message);
                    command.handle(ref clients, state);

                    if (clients.GetId(state.client.id).name == string.Empty) 
                    {
                        clients.Close(state.client.id);
                        return;
                    }

                    SendToAll(state.client, message, true);

                    state.sb.Clear();

                    //Send to other clients
                }

                state.client.connection
                    .BeginReceive(state.buffer, 0, StateObject.bufferSize, 0,  
                        new AsyncCallback(ReadCallback), state);
            }
        }
    }
}