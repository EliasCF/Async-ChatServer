namespace ChatServer
{
    public class CommandFactory
    {
        public ICommand Build (string command) 
        {
            ICommand builtCommand = null;

            string c = command.Split(' ')[0];

            switch (c)
            {
                case "/Name":
                    builtCommand = new NameCommand(command);
                    break;
                case "/Count":
                    builtCommand = new CountCommand();
                    break;
                default: 
                    builtCommand = new NoCommand();
                    break;
            }

            return builtCommand;
        }
    }
}