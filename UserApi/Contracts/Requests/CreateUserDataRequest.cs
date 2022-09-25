namespace UserApi.Contracts.Requests
{
    public class CreateUserDataRequest
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? ReviewsAmount { get; set; }
    }
}
