using System;

namespace ChatServer
{
    public class Logger 
    {
        /// <summary>
        /// Logs a message to the console
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Log (string message) 
        {
            Console.WriteLine(message);
        }
    }
}