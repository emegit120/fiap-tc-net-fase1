# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["*.sln", "./"]
COPY . ./
RUN dotnet restore

# Build and publish
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose port (adjust if your app uses a different port)
EXPOSE 8080

# Start the application
ENTRYPOINT ["dotnet", "FIAPTechChallenge.dll"]
