using MySpot.Api.Repositories;
using MySpot.

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddScoped<IClock,Clock>()
    .AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>()
    .AddSingleton<IReservationService, ReservationService>()
    .AddControllers();

var app = builder.Build();

app.MapControllers();
app.Run();