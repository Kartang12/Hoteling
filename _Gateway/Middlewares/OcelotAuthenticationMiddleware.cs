using _Gateway.Requests;

namespace _Gateway.Middlewares
{
    public class OcelotAuthenticationMiddleware
    {
        private readonly string authApiRoute = "https:\\localhost:5000\\";
        private readonly RequestDelegate _next;

        public OcelotAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //var token = context.Request.Headers.First(x => x.Key == "Authrization");
            //var roleRequest = new GetRolesRequest()
            //{
            //    Token = token.Value
            //};
            //using (var client = HttpClientFactory.Create())
            //{
            //    var role = client.GetAsync(authApiRoute + "GetRole");
            //    context.Request.Headers.Add("Role", "Test");
            //}

            await _next(context);
        }
    }
}
