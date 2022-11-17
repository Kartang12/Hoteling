namespace UserApi.Domain
{
    public class UserData
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime BirthDate { get; set; }
        public int ReviewsAmount { get; set; }
    }
}
