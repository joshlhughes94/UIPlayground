# Use Playwright .NET image that includes browsers + dependencies
FROM mcr.microsoft.com/playwright/dotnet:v1.55.0-noble AS build

WORKDIR /src

# Copy solution and project files
COPY ["UIPlayground.sln", "./"]
COPY ["UIPlayground/UIPlayground.csproj", "UIPlayground/"]

# Restore dependencies
RUN dotnet restore "UIPlayground/UIPlayground.csproj"

# Copy the rest of the code
COPY . .

WORKDIR "/src/UIPlayground"

# Build the project (release configuration)
RUN dotnet build "UIPlayground.csproj" -c Release -o /app/build

# (Optional) Publish if you need a published output
RUN dotnet publish "UIPlayground.csproj" -c Release -o /app/publish

# Set working directory for test run
WORKDIR "/src/UIPlayground"

# Run tests with detailed console output
ENTRYPOINT ["dotnet", "test", "--logger:trx;LogFileName=/app/test-results/results.trx"]