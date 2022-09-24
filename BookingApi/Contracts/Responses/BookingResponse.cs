namespace BookingApi.Contracts.Responses
{
    public class BookingResponse
    {
        public Guid Id { get; set; }
        public string HotelName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
