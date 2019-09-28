namespace ChatServer
{
    public interface ILogger
    {
        /// <summary>
        /// Log a message
        /// </summary>
        /// <param name="message">Message to log</param>
        void Log (string message);
    }
}