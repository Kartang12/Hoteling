namespace AuthorizationApi.Contracts.Requests
{
    public class GetRolesRequest
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}
