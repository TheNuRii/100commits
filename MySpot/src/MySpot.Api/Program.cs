using MySpot.Api.Repositories;
using MySpot.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddScoped<IClock,Clock>()
    .AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>()
    .AddSingleton<IReservationService, ReservationService>()
    .AddControllers();

var app = builder.Build();

app.MapControllers();
app.Run();