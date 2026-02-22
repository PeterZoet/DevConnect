# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files for restore
COPY DevConnect.Client/DevConnect.Client.csproj DevConnect.Client/
COPY DevConnect.Shared/DevConnect.Shared.csproj DevConnect.Shared/
RUN dotnet restore DevConnect.Client/DevConnect.Client.csproj

# Copy everything else and build
COPY . .
WORKDIR /src/DevConnect.Client
RUN dotnet publish -c Release -o /app

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

# Use HTTP only inside container
ENV DOTNET_URLS=http://+:7097
EXPOSE 7097

ENTRYPOINT ["dotnet", "DevConnect.Client.dll"]