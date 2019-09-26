namespace ChatServer
{
    public interface IParameterCommand
    {
         void handle(StateObject state);
         string command { get; }
         string parameter { get; }
    }
}