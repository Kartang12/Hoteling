namespace AuthorizationApi.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PWHash { get; set; }
    }
}
