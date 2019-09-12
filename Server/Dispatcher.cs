using System.Text;
using System.Net.Sockets;
using System.Net.IPEndPoint;
using System.Threading;
using System;

namespace ChatServer 
{
    public class Dispatcher 
    {
        private TcpNetworkManager network { get; }
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

        public void AcceptCallback (IAsyncResult result) 
        {
            allDone.Set();

            Socket listener = (Socket) result.AsyncState;
            Socket handler = listener.EndAccept(result);

            logger.Log($"Accepting connection from:{handler.RemoteEndPoint.ToString()}");

            StateObject state = new StateObject();
            state.client = handler;

            handler.BeginReceive(state.buffer, 0, StateObject.bufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public void ReadCallback (IAsyncResult result) 
        {       
            logger.Log("Reading message from!");

            string content = string.Empty;

            StateObject state = (StateObject)result.AsyncState;
            Socket handler = state.client;

            int byteRead = handler.EndReceive(result);

            if (byteRead > 0) 
            {
                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, byteRead));

                content = state.sb.ToString();
                
                if (content.IndexOf("<EOF>") > -1)
                {
                    logger.Log($"Read {content.Length} bytes from socket.\n Data: {content}");

                    //Send to other clients
                } 
                else 
                {
                    handler.BeginReceive(state.buffer, 0, StateObject.bufferSize, 0,  
                        new AsyncCallback(ReadCallback), state);
                }
            }
        }
    }
}