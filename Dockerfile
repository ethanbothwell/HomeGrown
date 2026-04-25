# ── Build stage ───────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /repo

# Copy solution and project files first for layer-cached restore
COPY HomeGrown.Backend.slnx ./
COPY src/HomeGrown.Core/HomeGrown.Core.csproj             src/HomeGrown.Core/
COPY src/HomeGrown.Infrastructure/HomeGrown.Infrastructure.csproj src/HomeGrown.Infrastructure/
COPY src/HomeGrown.API/HomeGrown.API.csproj               src/HomeGrown.API/

RUN dotnet restore src/HomeGrown.API/HomeGrown.API.csproj

# Copy everything else and publish
COPY src/ src/
RUN dotnet publish src/HomeGrown.API/HomeGrown.API.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# ── Runtime stage ─────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "HomeGrown.API.dll"]
