using Microsoft.OpenApi.Models;
using BookingApi.DbContext;
using Microsoft.EntityFrameworkCore;
using BookingApi.Services;
using AutoMapper;
using BookingApi.Mapping;
using MassTransit;
using BookingApi.Consumers;
using HotelingLibrary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookingContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddMvc();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Booking API", Version = "v1" });
});
builder.Services.AddControllers();

builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddMassTransit(config => {
    config.AddConsumer<HotelChangedConsumer>();
    config.AddConsumer<HotelDeletedConsumer>();
    config.AddConsumer<RoomChangedConsumer>();
    config.AddConsumer<RoomDeletedConsumer>();

    config.UsingRabbitMq((context, configuration) => {
        configuration.Host(builder.Configuration.GetConnectionString("RabbitMQ"));

        configuration.ReceiveEndpoint(QueuesUrls.Booking_HotelChanged, c => {
            c.ConfigureConsumer<HotelChangedConsumer>(context);
        });
        configuration.ReceiveEndpoint(QueuesUrls.Booking_HotelDeleted, c => {
            c.ConfigureConsumer<HotelDeletedConsumer>(context);
        });
        configuration.ReceiveEndpoint(QueuesUrls.Booking_RoomChanged, c => {
            c.ConfigureConsumer<RoomChangedConsumer>(context);
        });
        configuration.ReceiveEndpoint(QueuesUrls.Booking_RoomDeleted, c => {
            c.ConfigureConsumer<RoomDeletedConsumer>(context);
        });
    });
});


var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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

app.MapControllers();

app.Run();