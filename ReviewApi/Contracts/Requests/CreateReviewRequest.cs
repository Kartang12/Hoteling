namespace ReviewApi.Contracts.Requests
{
    public class CreateReviewRequest
    {
        public Guid HotelId { get; set; }
        public Guid UserId { get; set; }
        public string HotelName { get; set; }
        public string UserName { get; set; }
        public int Score { get; set; }
        public string Feedback { get; set; }
    }
}
