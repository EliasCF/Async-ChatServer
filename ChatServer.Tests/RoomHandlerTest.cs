using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace ChatServer.Tests
{
    public class RoomHandlerTest
    {
        public IServiceProvider services = new ServiceProviderBuilder().Build();

        /// <summary>
        /// Assert that Add method successfully adds a room to its list of Rooms when the method, 'Add' is called.
        /// This should not fail unless the methods fails to add the new Room to the list of rooms.
        /// </summary>
        [Fact]
        public void Add ()
        {
            //Arrage
            string roomName = "Room";
            RoomHandler rooms = services.GetService<RoomHandler>();

            //Act
            rooms.Add(roomName, Guid.NewGuid());

            //Assert
            Assert.Equal(1, rooms.Count);
        }

        /// <summary>
        /// Assert that the Remove method successfully removes the Room from the list of rooms.
        /// </summary>
        [Fact]
        public void Remove ()
        {
            //Arrange
            string roomName = "Room";
            RoomHandler rooms = services.GetService<RoomHandler>();
            Guid id = rooms.Add(roomName, Guid.NewGuid());

            //Act
            rooms.Remove(id);

            //Assert
            Assert.Equal(0, rooms.Count);
        }

        /// <summary>
        /// Assert that the FindByName method finds the right room using the specified id.
        /// </summary>
        [Fact]
        public void FindByName ()
        {
            //Arrange
            string roomName = "Room";
            RoomHandler rooms = services.GetService<RoomHandler>();
            Guid id = rooms.Add(roomName, Guid.NewGuid());

            //Act
            Guid foundRoomId = rooms.FindByName(roomName);

            //Assert
            Assert.Equal(id, foundRoomId);
        }
    }
}