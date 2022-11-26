namespace HotelApi.Domain
{
    public class Hotel
    {
        public Guid Id { get; set; }
        public string Name{ get; set; }
        public string Location { get; set; }
        public int RoomsAmount { get; set; }
        public double Rating { get; set; }
        public int TotalVisitors { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
    }
}
