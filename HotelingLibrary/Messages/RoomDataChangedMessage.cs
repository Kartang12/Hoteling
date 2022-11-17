namespace HotelingLibrary.Messages
{
    public class RoomDataChangedMessage : MessageBase
    {
        public Guid HotelId { get; set; }
        public int Number { get; set; }
        public bool Wifi { get; set; }
        public double Square { get; set; }
    }
}
