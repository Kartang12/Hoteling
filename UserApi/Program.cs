using HotelingLibrary;
using HotelingLibrary.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using UserApi.Consumers;
using UserApi.DbContext;
using UserApi.Mapping;
using UserApi.Services;

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

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddMassTransit(config => {
    config.AddConsumer<ReviewDeletedConsummer>();

    config.UsingRabbitMq((context, configuration) =>
    {
        configuration.Host(builder.Configuration.GetConnectionString("RabbitMQ"));
        configuration.ReceiveEndpoint(QueuesUrls.ReviewsDeleted, c =>{
            c.ConfigureConsumer<ReviewDeletedConsummer>(context);
        });
    });
});

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