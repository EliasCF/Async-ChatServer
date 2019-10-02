using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using Xunit;

namespace ChatServer.Tests
{
    public class ClientHandlerTest
    {
        public IServiceProvider services = new ServiceProviderBuilder().Build();
        
        /// <summary>
        /// Assert that ClientHandler successfully adds a user to its list of Clients when the method, 'Add' is called.
        /// This should not fail unless the methods fails to add the new Client to the list of Clients.
        /// </summary>
        [Fact]
        public void Add()
        {
            //Arrange
            ClientHandler clients = services.GetService<ClientHandler>();

            //Act
            clients.Add(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp), "");

            //Assert
            Assert.Equal(1, clients.Count);
        }

        /// <summary>
        /// Assert that all the GetAll method successfully gets alle available clients from the ClientHandlers list of Clients.
        /// This should never fail as long as the GetAll method returns the whole list of Clients from ClientHandler.
        /// </summary>
        [Fact]
        public void GetAll ()
        {
            //Arrange
            ClientHandler clients = services.GetService<ClientHandler>();

            for (int i = 0; i < 10; i++) 
            {
                clients.Add(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp), "");
            }

            //Act
            List<Client> allClients = clients.GetAll();

            //Assert
            Assert.Equal(clients.Count, allClients.Count);
        }

        /// <summary>
        /// Assert that the GetId method successfully gets the right Client using the id of an existing Client.
        /// And that using the GetId method with the id of a unadded Client returns null.
        /// </summary>
        [Fact]
        public void GetId ()
        {
            //Arrange
            ClientHandler clients = services.GetService<ClientHandler>();
            Guid id = clients.Add(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp), "");

            //Act
            Client clientById = clients.GetId(id);
            Client unaddedClient = clients.GetId(Guid.NewGuid());

            //Assert
            Assert.Equal(id, clientById.id);
            Assert.Null(unaddedClient);
        }

        /// <summary>
        /// Assert that the SetName method successfully sets the client's name to specified string.
        /// This should not fail as long as the method successfully assigns the name to the client and that the client
        /// going by the specified id exists in the ClientHandler's list of Clients.
        /// </summary>
        [Fact]
        public void SetName () 
        {
            //Arrange
            string newClientName = "John Doe";
            ClientHandler clients = services.GetService<ClientHandler>();
            Guid id = clients.Add(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp), "");

            //Act
            clients.SetName(id, newClientName);

            //Assert
            Assert.Equal(newClientName, clients.GetId(id).name);
        }

        /// <summary>
        /// Assert that the SetState method successfully sets the client's state to Passive.
        /// This should not fail as long as the method successfully changes the state of the client and that the client
        /// going by the specified id exists in the ClientHandler's list of Clients.
        /// </summary>
        [Fact]
        public void SetState ()
        {
            //Arrange
            ClientState state = ClientState.Passive;
            ClientHandler clients = services.GetService<ClientHandler>();
            Guid id = clients.Add(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp), "");

            //Act
            clients.SetState(id, state);

            //Assert
            Assert.Equal(state, clients.GetId(id).state);
        }

        /// <summary>
        /// Asset that the SetRoom method successfully sets the client's roomId to the specified Guid.
        /// This should not fail as long as the method successfully changes the roomId of the client and that the client
        /// going by the specified id exists in the ClientHandler's list of Clients.
        /// </summary>
        [Fact]
        public void SetRoom ()
        {
            //Arrange
            Guid room = Guid.NewGuid();
            ClientHandler clients = services.GetService<ClientHandler>();
            Guid id = clients.Add(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp), "");

            //Act
            clients.SetRoom(id, room);

            //Assert
            Assert.Equal(room, clients.GetId(id).roomId);
        }

        /// <summary>
        /// Asserts that the Close method successfully remove the Client from ClientHandler's list of Clients.
        /// </summary>
        [Fact]
        public void Close ()
        {
            //Arrange
            ClientHandler clients = services.GetService<ClientHandler>();
            Guid id = clients.Add(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp), "");

            //Act  
            clients.Close(id);

            //Assert
            Assert.Equal(0, clients.Count);
        }
        
        /// <summary>
        /// Asserts that the Exists method successfully recognises that existent Client exists and that the nonexistent Client doesn't.
        /// </summary>
        [Fact]
        public void Exists ()
        {
            //Arrange
            ClientHandler clients = services.GetService<ClientHandler>();
            Guid id = clients.Add(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp), "");

            //Act  
            bool existentClient = clients.Exists(id);
            bool unexistentClient = clients.Exists(Guid.NewGuid());

            //Assert
            Assert.True(existentClient);
            Assert.False(unexistentClient);
        }
    }
}
