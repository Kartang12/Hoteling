using _Gateway.Middlewares;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("ocelot.json");

builder.Services.AddOcelot();
builder.Services.AddHttpClient("RoleClient", config => {
    config.BaseAddress = new Uri(builder.Configuration["AuthServece:GetRoleUrl"]);
});
builder.Services.AddSingleton<OcelotAuthorizationMiddleware>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseHttpsRedirection();
app.UseRouting();

var authMiddleware = app.Services.GetRequiredService<OcelotAuthorizationMiddleware>();
var ocelotConfiguration = new OcelotPipelineConfiguration
{
    AuthorizationMiddleware = async (context, next) => await authMiddleware.InvokeAsync(context, next)
};

app.UseOcelot(ocelotConfiguration).Wait();
app.Run();
