FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Instalar dependencias para AOT (clang, etc.)
RUN apt-get update && apt-get install -y clang zlib1g-dev

# Copiar csproj y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar todo y compilar
COPY . ./
RUN dotnet publish -c Release -o out -r linux-arm64 --self-contained /p:PublishAot=true

# Imagen final basada en runtime-deps (más ligera, sólo para AOT)
FROM mcr.microsoft.com/dotnet/runtime-deps:9.0-alpine
WORKDIR /app
COPY --from=build /app/out .

# Puerto y comando de inicio
ENV PORT=8080
EXPOSE ${PORT}
ENTRYPOINT ["sh", "-c", "ASPNETCORE_URLS=http://0.0.0.0:${PORT} ./MinimalApiDemoAOT"]
