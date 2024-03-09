#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Domestica.Budget.API/Domestica.Budget.API.csproj", "src/Domestica.Budget.API/"]
COPY ["src/Domestica.Budget.Application/Domestica.Budget.Application.csproj", "src/Domestica.Budget.Application/"]
COPY ["src/Domestica.Budget.Domain/Domestica.Budget.Domain.csproj", "src/Domestica.Budget.Domain/"]
COPY ["src/Domestica.Budget.Infrastructure/Domestica.Budget.Infrastructure.csproj", "src/Domestica.Budget.Infrastructure/"]
RUN dotnet restore "./src/Domestica.Budget.API/./Domestica.Budget.API.csproj"
COPY . .
WORKDIR "/src/src/Domestica.Budget.API"
RUN dotnet build "./Domestica.Budget.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Domestica.Budget.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Domestica.Budget.API.dll"]