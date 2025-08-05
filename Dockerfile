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


# Install the agent
RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
&& echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
&& wget https://download.newrelic.com/548C16BF.gpg \
&& apt-key add 548C16BF.gpg \
&& apt-get update \
&& apt-get install -y 'newrelic-dotnet-agent' \
&& rm -rf /var/lib/apt/lists/*

# Enable the agent
ENV CORECLR_ENABLE_PROFILING=1 \
CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
CORECLR_NEWRELIC_HOME=/usr/local/newrelic-dotnet-agent \
CORECLR_PROFILER_PATH=/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so \
NEW_RELIC_LICENSE_KEY=6f9d13374ae840bdc59db2c8261e1f8aFFFFNRAL \
NEW_RELIC_APP_NAME="dotnet-newrelic"

# Start the application
ENTRYPOINT ["dotnet", "FIAPTechChallenge.dll"]
