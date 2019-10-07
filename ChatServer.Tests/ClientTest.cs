using System.Net.Sockets;
using System;
using Xunit;

namespace ChatServer.Tests
{
    public class ClientTest
    {
        [Fact]
        public void AcceptsCommand ()
        {
            //Arrange
            Client client = new Client(Guid.NewGuid(), new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp), "", ClientState.NeedName);
            string incompatbileCommand = "/RoomCreate The fun-room";
            string compatibleCommand = "/Name John Doe";

            //Act
            bool acceptsCommand = client.AcceptsCommand(compatibleCommand);
            bool DoesntAcceptCommand = client.AcceptsCommand(incompatbileCommand);

            //Assert
            Assert.True(acceptsCommand);
            Assert.False(DoesntAcceptCommand);
        } 
    }
}