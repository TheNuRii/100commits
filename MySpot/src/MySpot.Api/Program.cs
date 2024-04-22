using MySpot.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddScoped<Clock>()
    .AddControllers();

var app = builder.Build();

app.MapControllers();
app.Run();