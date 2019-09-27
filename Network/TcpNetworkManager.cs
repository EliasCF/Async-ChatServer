using System.Net.Sockets;
using System.Net;
using System;  

namespace ChatServer
{
    public class TcpNetworkManager 
    {
        private ILogger logger = new ConsoleLogger();

        private TcpListener listener { get; }

        public TcpNetworkManager (int port) 
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            listener = new TcpListener(ipHostInfo.AddressList[0], port);

            logger.Log($"Server started on {listener.LocalEndpoint.ToString()}");
        }

        public void ListenForConnections () 
        {
            listener.Start();
            logger.Log("The server is now listening for connections");
        }

        public void AcceptClients (AsyncCallback callback)
        {
            listener.BeginAcceptTcpClient(callback, listener.Server);
        }
    }
}