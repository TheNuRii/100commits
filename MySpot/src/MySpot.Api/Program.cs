<<<<<<< HEAD
<<<<<<< HEAD
=======
using MySpot.Api.Entities;
>>>>>>> parent of f72881c (Creating new project to capsulete ValueObjects, Exceptions, Entities)
using MySpot.Api.Repositories;
<<<<<<< HEAD
using MySpot.
=======
using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;
<<<<<<< HEAD
>>>>>>> parent of 8048d5b (Dependency Inversion Principle)
=======
using MySpot.Api.Services;
>>>>>>> parent of d63e74c (Create Aplication Layer and refactoring codebase)
=======
>>>>>>> parent of f72881c (Creating new project to capsulete ValueObjects, Exceptions, Entities)

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddScoped<IClock,Clock>()
    .AddSingleton<IEnumerable<WeeklyParkingSpot>>(serviceProvider =>
    {
        var clock = serviceProvider.GetRequiredService<Clock>();
        return new List<WeeklyParkingSpot>()
        {
            new(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(clock.Current()), "P1"),
            new(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(clock.Current()), "P2"),
            new(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(clock.Current()), "P3"),
            new(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(clock.Current()), "P4"),
            new(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(clock.Current()), "P5"),
        };
    })
    .AddSingleton<IReservationService, ReservationService>()
    .AddControllers();

var app = builder.Build();

app.MapControllers();
app.Run();