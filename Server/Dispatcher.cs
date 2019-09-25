using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System;

namespace ChatServer 
{
    public class Dispatcher 
    {
        private TcpNetworkManager network { get; }
        private Logger logger = new Logger();
        private ManualResetEvent allDone = new ManualResetEvent(false);
        public ServiceProvider services { get; set; }

        /// <summary>
        /// Initialize new network connection
        /// </summary>
        /// <param name="port"></param>
        public Dispatcher (IServiceCollection service, int port)
        {
            services = service.BuildServiceProvider();
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

            ServerIO io = new ServerIO(services);
            io.ResetEventIsSet += SetManualResetEvent;

            while (true) 
            {  
                allDone.Reset();
                network.AcceptClients(new AsyncCallback(io.AcceptCallback));
                allDone.WaitOne();
            }
        }

        private void SetManualResetEvent (object sender, EventArgs e) 
        {
            allDone.Set();
        }
    }
}