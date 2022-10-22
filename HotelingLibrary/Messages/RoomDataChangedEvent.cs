namespace HotelingLibrary.Messages
{
    public class RoomDataChangedEvent : EventBase
    {
        public Guid HotelId { get; set; }
        public int Number { get; set; }
        public bool Wifi { get; set; }
        public double Square { get; set; }
    }
}
