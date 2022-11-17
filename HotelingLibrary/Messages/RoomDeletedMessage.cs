namespace HotelingLibrary.Messages
{
    public class RoomDeletedMessage: MessageBase
    {
        public Guid NewRoomId { get; set; }
        public int NewRoomNumber { get; set; }
    }
}
