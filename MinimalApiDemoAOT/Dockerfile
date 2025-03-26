FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
# Install clang/zlib1g-dev dependencies for publishing to native
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    clang zlib1g-dev
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

# Copiar proyecto y restaurar dependencias
COPY MinimalApiDemoAOT.csproj .
RUN dotnet restore

# Copiar el resto del código y construir
COPY . .
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /app
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

FROM mcr.microsoft.com/dotnet/runtime-deps:9.0 AS final
WORKDIR /app
EXPOSE 8080
ENV PORT=8080
COPY --from=publish /app/publish .
ENTRYPOINT ["./MinimalApiDemoAOT"]
