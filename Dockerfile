FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
#RUN dotnet publish -c Release -o out
RUN dotnet publish "./flickr-app.csproj" --output "./out" --configuration Release --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "flickr-app.dll"]
