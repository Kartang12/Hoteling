namespace HotelingLibrary.Messages
{
    public class HotelDataChangedEvent : EventBase
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int RoomsAmount { get; set; }
        public double Rating { get; set; }
        public int TotalVisitors { get; set; }
    }
}
