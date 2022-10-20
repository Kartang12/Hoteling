namespace AuthorizationApi.Contracts.Responses
{
    public class RegistrationResponse
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
