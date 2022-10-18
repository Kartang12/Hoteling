using _Gateway.Requests;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("ocelot.json");

var ocelotConfiguration = new OcelotPipelineConfiguration
{
    AuthenticationMiddleware = async (context, next) =>
    {

        //var token = context.Request.Headers.First(x => x.Key == "Authrization");
        //var roleRequest = new GetRolesRequest()
        //{
        //    Token = token.Value
        //};
        //using (var client = HttpClientFactory.Create())
        //{
        //    var role = client.GetAsync("https:\\localhost:5000\\GetRole");
            //context.Request.Headers.Add("Role", "Admin");
        //}

        await next.Invoke();
    }
};

builder.Services.AddOcelot();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseHttpsRedirection();
app.UseRouting();

//app.UseAuthorization();
app.UseOcelot(ocelotConfiguration).Wait();
app.Run();
