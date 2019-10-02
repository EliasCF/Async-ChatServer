namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 7777; //Port to open server on

            Dispatcher dispatcher = new Dispatcher(new ServiceProviderBuilder().Get(), port);
            dispatcher.Dispatch();
        }
    }
}
