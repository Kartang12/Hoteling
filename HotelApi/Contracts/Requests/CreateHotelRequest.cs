namespace HotelApi.Contracts.Requests
{
    public class CreateHotelRequest
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int RoomsAmount { get; set; }
        public IEnumerable<CreateHotelRoomModel> Rooms { get; set; }
    }

    public class CreateHotelRoomModel
    {
        public int Number { get; set; }
        public bool Wifi { get; set; }
        public double Square { get; set; }
    }
}
