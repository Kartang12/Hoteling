using AutoMapper;
using HotelingLibrary;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ReviewApi.Consumers;
using ReviewApi.DbContext;
using ReviewApi.Mapping;
using ReviewApi.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ReviewsContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddMvc();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Review API", Version = "v1" });
});

builder.Services.AddControllers();

builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddMassTransit(config => {
    config.AddConsumer<HotelChangedConsumer>();
    config.AddConsumer<HotelDeletedConsumer>();

    config.UsingRabbitMq((context, configuration) => {
        configuration.Host(builder.Configuration.GetConnectionString("RabbitMQ"));

        configuration.ReceiveEndpoint(QueuesUrls.Review_HotelChanged, c => {
            c.ConfigureConsumer<HotelChangedConsumer>(context);
        });
        configuration.ReceiveEndpoint(QueuesUrls.Review_HotelDeleted, c => {
            c.ConfigureConsumer<HotelDeletedConsumer>(context);
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
//app.UseAuthentication();
//app.UseAuthorization();
app.MapControllers();

app.Run();