using System.Text;

namespace ChatServer
{
    public class StateObject
    {
        /// <summary>
        /// Set client property
        /// </summary>
        /// <param name="socket">Socket to be set as the client property</param>
        public StateObject (Client c = null) 
        {
            client = c;
        }

        public Client client;

        public const int bufferSize = 1024;

        public byte[] buffer = new byte[bufferSize];

        public StringBuilder sb = new StringBuilder();
    }
}