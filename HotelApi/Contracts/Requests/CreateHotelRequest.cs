namespace HotelApi.Contracts.Requests
{
    public class CreateHotelRequest
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int RoomsAmount { get; set; }
        public IEnumerable<CreateRoomRequest>? Rooms { get; set; }
    }
}
