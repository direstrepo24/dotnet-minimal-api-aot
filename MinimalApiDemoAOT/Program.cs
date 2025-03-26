using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using System.Text.Json.Serialization;

// Uso de CreateSlimBuilder para optimizar AOT
var builder = WebApplication.CreateSlimBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.TypeInfoResolver = ApiJsonSerializerContext.Default;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoint de health check para Railway
app.MapGet("/healthz", () => Results.Ok("Healthy"));

// Endpoint para la raíz
app.MapGet("/", () => "¡Hola desde .NET Minimal API desplegado en Railway con GitHub Actions!");

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

app.Run();

// Definición completa de tipos serializables para AOT
[JsonSerializable(typeof(WeatherForecast[]))]  
[JsonSerializable(typeof(WeatherForecast))]  
[JsonSerializable(typeof(string))]  // Para respuestas simples como el health check
internal partial class ApiJsonSerializerContext : JsonSerializerContext
{
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
