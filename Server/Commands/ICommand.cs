namespace ChatServer
{
    public interface ICommand
    {
         void handle(ref ClientHandler clients, ref RoomHandler chatRooms, StateObject state);
         string command { get; }
    }
}