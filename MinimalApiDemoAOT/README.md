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

Este proyecto está configurado para ser desplegado automáticamente en Railway mediante GitHub Actions.

### GitHub Actions

El proyecto incluye un workflow de GitHub Actions (`railway-deploy.yml`) que se encarga de:

1. Construir la imagen Docker de la aplicación
2. Publicar la imagen en Docker Hub
3. Desplegar la aplicación en Railway

El workflow se ejecuta automáticamente cuando se realiza un push a la rama `main`.

### Secretos Necesarios

Para que el workflow funcione correctamente, es necesario configurar los siguientes secretos en el repositorio de GitHub:

- `DOCKERHUB_TOKEN`: Token de acceso personal de Docker Hub
- `RAILWAY_TOKEN`: Token de API de Railway
- `RAILWAY_SERVICE`: Nombre del servicio en Railway

### Configuración de Railway

La configuración para Railway se encuentra en el archivo `railway.toml`, que especifica:

- El uso del Dockerfile para la construcción
- El comando de inicio de la aplicación
- La configuración del puerto mediante variables de entorno

## Licencia

MIT
