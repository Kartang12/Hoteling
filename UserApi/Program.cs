using UserApi.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UserApi.Services;
using AutoMapper;
using UserApi.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UsersContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddMvc();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User API", Version = "v1" });
});

builder.Services.AddControllers();

builder.Services.AddTransient<IUserService, UserService>();

//var mapperConfig = new MapperConfiguration(mc =>
//{
//    mc.AddProfile(new MappingProfile());
//});

builder.Services.AddAutoMapper(mc =>
{
    mc.AddProfile(new MappingProfile());
});

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