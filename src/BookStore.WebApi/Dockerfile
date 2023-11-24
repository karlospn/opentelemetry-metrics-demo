FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build-env
WORKDIR /app

# Copy everything
COPY . ./

# Restore packages
RUN dotnet restore -s "https://api.nuget.org/v3/index.json" \
	--runtime linux-x64	

# Build project
RUN dotnet build "./src/BookStore.WebApi/BookStore.WebApi.csproj" \ 
    -c Release \
	--runtime linux-x64 \ 
	--self-contained true \	
	--no-restore

# Publish app
RUN dotnet publish "./src/BookStore.WebApi/BookStore.WebApi.csproj" \
	-c Release \
	-o /app/publish \
	--no-restore \ 
	--no-build \
	--self-contained true \
	--runtime linux-x64

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime-deps:8.0.0-jammy-chiseled-extra

# Copy artifact
WORKDIR /app
COPY --from=build-env /app/publish .

# Starts on port 8080
ENV ASPNETCORE_URLS=http://+:8080



# Set Entrypoint
ENTRYPOINT ["./BookStore.WebApi"]
