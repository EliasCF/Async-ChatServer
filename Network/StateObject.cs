using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    public class StateObject
    {
        public Socket client = null;

        public const int bufferSize = 1024;

        public byte[] buffer = new byte[bufferSize];

        public StringBuilder sb = new StringBuilder();
    }
}