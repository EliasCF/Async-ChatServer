namespace ChatServer
{
    public interface IParameterCommand
    {
        /// <summary>
        /// Contains the functionality of the command
        /// </summary>
        /// <param name="state">StateObject for the client that send the command</param>
        void handle(StateObject state);

        /// <summary>
        /// Contain the command as text.
        /// It will be used to validate against any potential command a user might send
        /// </summary>
        string command { get; }

        /// <summary>
        /// Contains the parameter values that the client send along with the command itself
        /// </summary>
        /// <value></value>
        string parameter { get; }
    }
}