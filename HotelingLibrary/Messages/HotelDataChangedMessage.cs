namespace HotelingLibrary.Messages
{
    public class HotelDataChangedMessage : MessageBase
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int RoomsAmount { get; set; }
        public double Rating { get; set; }
        public int TotalVisitors { get; set; }
    }
}
