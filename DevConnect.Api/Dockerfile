# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY DevConnect.Api/DevConnect.Api.csproj DevConnect.Api/
COPY DevConnect.Shared/DevConnect.Shared.csproj DevConnect.Shared/
RUN dotnet restore DevConnect.Api/DevConnect.Api.csproj

# Copy everything
COPY DevConnect.Api/ DevConnect.Api/
COPY DevConnect.Shared/ DevConnect.Shared/

WORKDIR /src/DevConnect.Api
RUN dotnet publish -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app ./

# Copy JSON to /app/Data
RUN mkdir -p Data
COPY DevConnect.Shared/Data/Articles.json ./Data/

ENTRYPOINT ["dotnet", "DevConnect.Api.dll"]