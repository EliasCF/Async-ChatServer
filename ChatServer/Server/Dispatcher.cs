using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System;

namespace ChatServer 
{
    public class Dispatcher 
    {
        private TcpNetworkManager network { get; }
        private ILogger logger { get; }
        private ManualResetEvent allDone = new ManualResetEvent(false);
        public ServiceProvider services { get; set; }

        /// <summary>
        /// Initialize new network connection
        /// </summary>
        /// <param name="port"></param>
        public Dispatcher (IServiceCollection service, int port)
        {
            services = service.BuildServiceProvider();

            logger = services.GetService<ILogger>();
            network = new TcpNetworkManager(services.GetService<ILogger>(), port);
        }

        /// <summary>
        /// Starts server and begis the acceptor loop
        /// </summary>
        public void Dispatch () 
        {
            logger.Log("Starting AcceptorLoop");
            AcceptorLoop();
        }

        /// <summary>
        /// Accept clients
        /// </summary>
        private void AcceptorLoop () 
        {
            network.ListenForConnections();

            ClientAcceptor acceptor = new ClientAcceptor(services);
            acceptor.ResetEventIsSet += SetManualResetEvent;

            while (true) 
            {  
                allDone.Reset();
                network.AcceptClients(acceptor.AcceptCallback);
                allDone.WaitOne();
            }
        }
        private void SetManualResetEvent (object sender, EventArgs e) 
        {
            allDone.Set();
        }
    }
}