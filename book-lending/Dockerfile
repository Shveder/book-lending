# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET SDK 8.0 as a parent image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy and restore the project files
COPY ["book-lending.csproj", "./"]
RUN dotnet restore "./book-lending.csproj"

# Copy the remaining project files and build the app
COPY . .
WORKDIR "/src/."
RUN dotnet build "book-lending.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "book-lending.csproj" -c Release -o /app/publish

# Build the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "book-lending.dll"]
