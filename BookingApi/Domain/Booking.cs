namespace BookingApi.Domain
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public int RoomNumber { get; set; }
        public Guid HotelId { get; set; }
        public string HotelName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestsAmount { get; set; }
    }
}
