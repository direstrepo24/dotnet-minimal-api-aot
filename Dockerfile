FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
# Install clang/zlib1g-dev dependencies for publishing to native
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    clang zlib1g-dev
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY MinimalApiDemoAOT.sln ./
COPY MinimalApiDemoAOT/ ./MinimalApiDemoAOT/

RUN dotnet restore MinimalApiDemoAOT.sln
RUN dotnet build MinimalApiDemoAOT/MinimalApiDemoAOT.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish MinimalApiDemoAOT/MinimalApiDemoAOT.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

FROM mcr.microsoft.com/dotnet/runtime-deps:9.0 AS final
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

COPY --from=publish /app/publish .
ENTRYPOINT ["./MinimalApiDemoAOT"]
