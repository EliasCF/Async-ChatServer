using Microsoft.Extensions.DependencyInjection;

namespace ChatServer
{
    /// <summary>
    /// A command that does nothing
    /// </summary>
    public class NoCommand : ICommand
    {
        public string command { get; } = "";

        public NoCommand (ServiceProvider service) { }

        public void handle(StateObject state) { }
    }
}