FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY MinimalApiDemoAOT.sln ./

COPY MinimalApiDemoAOT/ ./MinimalApiDemoAOT/

RUN dotnet restore MinimalApiDemoAOT.sln

RUN dotnet publish MinimalApiDemoAOT/MinimalApiDemoAOT.csproj \
    -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "MinimalApiDemoAOT.dll"]
