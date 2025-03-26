var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseHttpsRedirection();

// Endpoint para la raíz
app.MapGet("/", () => "¡Hola desde .NET Minimal API desplegado en Railway con GitHub Actions!");

// Endpoint de health check para Railway
app.MapGet("/healthz", () => Results.Ok("Healthy"));

// Endpoint de información básico
app.MapGet("/api/info", () => new ApiInfo
{
    Name = "API .NET Minimal API en Railway",
    Version = "1.1",
    Working = true,
    Platform = "Railway",
    Timestamp = DateTime.UtcNow
});

// Weather forecast endpoint
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// Iniciar la aplicación
app.Run();

// Records y classes
public record ApiInfo
{
    public string Name { get; init; } = "";
    public string Version { get; init; } = "";
    public bool Working { get; init; }
    public string Platform { get; init; } = "";
    public DateTime Timestamp { get; init; }
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
