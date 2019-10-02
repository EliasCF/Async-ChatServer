using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace ChatServer.Tests
{
    public class CommandFactoryTest
    {
        public IServiceProvider services = new ServiceProviderBuilder().Build();

        [Fact]
        public void Build () 
        {
            //Arrange
            CommandFactory factory = new CommandFactory();

            //Act
            ICommand nameCommand = factory.Build(services as ServiceProvider, "/Name John Doe");

            //Assert
            Assert.IsType<NameCommand>(nameCommand);
        }
    }
}