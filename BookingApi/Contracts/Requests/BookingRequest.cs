namespace BookingApi.Contracts.Requests
{
    public class BookingRequest
    {
        public Guid HotelId { get; set; }
        public Guid RoomId { get; set; }
        public int RoomNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestsAmount { get; set; }
    }
}
