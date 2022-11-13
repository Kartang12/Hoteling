using Newtonsoft.Json.Linq;
using Ocelot.Middleware;
using System.Net.Http.Headers;

namespace _Gateway.Middlewares
{
    public class OcelotAuthorizationMiddleware
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OcelotAuthorizationMiddleware(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task InvokeAsync(HttpContext context, Func<Task> next)
        {
            if (!context.Request.Path.Value.Contains("/Auth"))
            {
                //var token = context.Request.Headers.Authorization.First().Remove(0, 7);
                //var client = HttpClientFactory.Create();
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
                //var response = await client.GetAsync("https://localhost:5000/Auth/GetRole");
                
                //JObject role = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                //var x = role["result"].ToString();
                //context.Request.Headers.Clear();
                //context.Request.Headers.Add("Role", "2");
                //var route = context.Items.UpstreamRoute();
                //var route = context.Items.DownstreamRoute();
                //var headers = route.AddHeadersToDownstream;
                //    headers.Add(new Ocelot.Configuration.Creator.AddHeader("Role", "2"));
            }

            await next.Invoke();
        }
    }
}
