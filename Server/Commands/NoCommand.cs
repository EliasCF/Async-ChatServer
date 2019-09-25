using Microsoft.Extensions.DependencyInjection;

namespace ChatServer
{
    public class NoCommand : ICommand
    {
        public string command { get; } = "";

        public NoCommand (ServiceProvider service) { }

        public void handle(StateObject state) { }
    }
}