namespace HotelApi.Contracts.Requests
{
    public class CreateRoomRequest
    {
        public Guid HotelId { get; set; }
        public int Number { get; set; }
        public bool Wifi { get; set; }
        public double Square { get; set; }
    }
}
