# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["LibarayMS.sln", "."]
COPY ["LibararyMS.Domain/LibararyMS.Domain.csproj", "LibararyMS.Domain/"]
COPY ["LibraryMS.Application/LibraryMS.Application.csproj", "LibraryMS.Application/"]
COPY ["LibraryMS.Infrastructure/LibraryMS.Infrastructure.csproj", "LibraryMS.Infrastructure/"]
COPY ["LibarayMS.Presentation/LibarayMS.Presentation.csproj", "LibarayMS.Presentation/"]

RUN dotnet restore "LibarayMS.sln"

# Copy everything else and build
COPY . .
WORKDIR "/src/LibarayMS.Presentation"
RUN dotnet build "LibarayMS.Presentation.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "LibarayMS.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Create non-root user
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser:appuser /app
USER appuser

COPY --from=publish /app/publish .

EXPOSE 80

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD curl -f http://localhost/health || exit 1

ENTRYPOINT ["dotnet", "LibarayMS.Presentation.dll"]
