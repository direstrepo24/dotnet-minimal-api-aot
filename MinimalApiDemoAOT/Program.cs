using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

// La configuración del puerto se hace a través de la variable de entorno ASPNETCORE_URLS
// que es establecida en el Dockerfile y railway.toml
// No necesitamos configurar explícitamente el puerto aquí

// Configurar JSON serialization para AOT
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

// Endpoint para la raíz
app.MapGet("/", () => "¡Hola desde .NET Native AOT desplegado en Railway!");

// Endpoint de información básico
app.MapGet("/api/info", () => new ApiInfo
{
    Name = "API .NET Native AOT en Railway",
    Version = "1.1",
    Working = true,
    Platform = "Railway",
    Timestamp = DateTime.UtcNow
});

// Iniciar la aplicación
app.Run();

// Records y classes para serialización AOT
public record ApiInfo
{
    public string Name { get; init; } = "";
    public string Version { get; init; } = "";
    public bool Working { get; init; }
    public string Platform { get; init; } = "";
    public DateTime Timestamp { get; init; }
}

// Contexto de serialización requerido para AOT
[JsonSerializable(typeof(ApiInfo))]
[JsonSerializable(typeof(string))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
