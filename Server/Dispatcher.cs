using System.Net.Sockets;
using System.Threading;
using System.Text;
using System;

namespace ChatServer 
{
    public class Dispatcher 
    {
        private TcpNetworkManager network { get; }
        private ClientHandler clients = new ClientHandler(); //Connected clients
        private Logger logger = new Logger();

        private ManualResetEvent allDone = new ManualResetEvent(false);

        /// <summary>
        /// Initialize new network connection
        /// </summary>
        /// <param name="port"></param>
        public Dispatcher (int port)
        {
            network = new TcpNetworkManager(port);
        }

        /// <summary>
        /// Starts server and begis the event-loop
        /// </summary>
        public void Dispatch () 
        {
            logger.Log("Starting EventLoop");
            EventLoop();
        }

        /// <summary>
        /// Handle event loop
        /// </summary>
        private void EventLoop () 
        {
            network.ListenForConnections();

            while (true) 
            {  
                allDone.Reset();

                network.AcceptClients(new AsyncCallback(AcceptCallback));

                allDone.WaitOne();
            }
        }

        /// <summary>
        /// Accept client and receive their first message
        /// </summary>
        /// <param name="result">Socket of the accepted client</param>
        public void AcceptCallback (IAsyncResult result) 
        {
            allDone.Set();

            Socket listener = (Socket)result.AsyncState;
            Socket handler = listener.EndAccept(result);

            logger.Log($"Accepting connection from: '{handler.RemoteEndPoint.ToString()}'");

            Guid newClientId = clients.Add(handler, string.Empty);
            StateObject state = new StateObject(clients.GetId(newClientId));

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

                    //Send to other clients
                } 
                else 
                {
                    state.client.connection
                        .BeginReceive(state.buffer, 0, StateObject.bufferSize, 0,  
                            new AsyncCallback(ReadCallback), state);
                }
            }
        }
    }
}