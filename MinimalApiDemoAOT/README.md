# MinimalApiDemoAOT

Una API mínima de .NET implementada con Native AOT (Ahead-of-Time) para optimizar el rendimiento y reducir el tamaño de la aplicación.

## Características

- Implementación de .NET Minimal API con Native AOT
- Compilación optimizada para reducir el tiempo de inicio y el consumo de memoria
- Endpoints REST simples para demostración
- Configuración de Docker para despliegue en contenedores

## Requisitos

- .NET 9.0 SDK o superior
- Docker (para construcción y ejecución en contenedores)

## Estructura del Proyecto

- `Program.cs`: Punto de entrada de la aplicación y definición de endpoints
- `Dockerfile`: Configuración para construcción de imagen Docker con soporte AOT

## Endpoints

- `GET /`: Retorna un mensaje de bienvenida
- `GET /api/info`: Retorna información sobre la aplicación en formato JSON

## Cómo Ejecutar

### Localmente

```bash
dotnet run
```

### Con Docker

```bash
docker build -t minimal-api-aot .
docker run -p 8080:8080 minimal-api-aot
```

## Despliegue

Este proyecto está configurado para ser desplegado en Railway.

## Licencia

MIT
