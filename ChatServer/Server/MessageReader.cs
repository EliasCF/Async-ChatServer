using Microsoft.Extensions.DependencyInjection;
using System.Net.Sockets;
using System.Text;
using System;

namespace ChatServer
{
    public class MessageReader
    {
        private ServiceProvider services { get; }

        private ILogger logger { get; }

        private ClientHandler clients { get; }

        private MessageSender sender { get; }

        public MessageReader (IServiceProvider service) 
        {
            logger = service.GetService<ILogger>();
            sender = service.GetService<MessageSender>();
            clients = service.GetService<ClientHandler>();

            services = (ServiceProvider)service;
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

                    if (message[0] == '/') 
                    {
                        //If the message was a command then run it
                        CommandFactory factory = new CommandFactory();
                        ICommand command = factory.Build(services, message);
                        command.handle(state);
                    }

                    //Exit method if the last command was a DiconnectCommand
                    if (!clients.Exists(state.client.id)) return;

                    //Disconnect client if their first message wasn't a name command
                    if (clients.GetId(state.client.id).state == ClientState.NeedName) 
                    {
                        clients.Close(state.client.id);
                        return;
                    }

                    sender.SendToAll(state.client, message, true); //Send message to all clients but the sender

                    state.sb.Clear(); //Clear StringBuilder of messages
                }

                state.client.connection
                    .BeginReceive(state.buffer, 0, StateObject.bufferSize, 0,  
                        new AsyncCallback(ReadCallback), state);
            }
        }
    }
}