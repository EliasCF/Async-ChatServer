using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
using System.Text;
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

            ServerIO io = new ServerIO(ref allDone);

            while (true) 
            {  
                allDone.Reset();
                network.AcceptClients(new AsyncCallback(io.AcceptCallback));
                allDone.WaitOne();
            }
        }
    }
}