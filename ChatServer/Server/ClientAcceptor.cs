using Microsoft.Extensions.DependencyInjection;
using System.Net.Sockets;
using System;

namespace ChatServer
{
    public class ClientAcceptor
    {
        public event EventHandler ResetEventIsSet;

        private ILogger logger { get; }

        private ClientHandler clients { get; }

        private MessageSender sender { get; }

        private MessageReader reader { get; }            

        public ClientAcceptor (IServiceProvider services)
        {
            logger = services.GetService<ILogger>();
            clients = services.GetService<ClientHandler>();
            sender = services.GetService<MessageSender>();

            reader = new MessageReader(services); //services.GetService<MessageReader>();
        }

        protected virtual void OnManualResetEventSet (EventArgs e)
        {
            EventHandler handler = ResetEventIsSet;
            handler?.Invoke(this, e);
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

            sender.Send(state.client, "Welcome, you need to set your name by typing the name command: '/Name <name>' \r\nExample: '/Name Lars'\r\n");

            OnManualResetEventSet(new EventArgs());

            state.client.connection
                .BeginReceive(state.buffer, 0, StateObject.bufferSize, 0,
                    new AsyncCallback(reader.ReadCallback), state);
        }
    }
}