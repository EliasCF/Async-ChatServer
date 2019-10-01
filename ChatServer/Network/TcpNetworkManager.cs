using System.Net.Sockets;
using System.Net;
using System;  

namespace ChatServer
{
    public class TcpNetworkManager 
    {
        private ILogger logger { get; }

        private TcpListener listener { get; }

        public TcpNetworkManager (ILogger log, int port) 
        {
            logger = log;
            
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            listener = new TcpListener(ipHostInfo.AddressList[0], port);
        }

        public void ListenForConnections () 
        {
            logger.Log($"The server is now listening for connections on: {listener.LocalEndpoint.ToString()}");
            listener.Start();
        }

        public void AcceptClients (AsyncCallback callback)
        {
            listener.BeginAcceptTcpClient(callback, listener.Server);
        }
    }
}