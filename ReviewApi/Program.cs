using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ReviewApi.DbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ReviewsContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddMvc();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Review API", Version = "v1" });
});

builder.Services.AddControllers();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
//app.UseAuthentication();
//app.UseAuthorization();
app.MapControllers();

app.Run();