#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Budgetify.API/Budgetify.API.csproj", "src/Budgetify.API/"]
COPY ["src/Budgetify.Application/Budgetify.Application.csproj", "src/Budgetify.Application/"]
COPY ["src/Budgetify.Domain/Budgetify.Domain.csproj", "src/Budgetify.Domain/"]
COPY ["src/Budgetify.Infrastructure/Budgetify.Infrastructure.csproj", "src/Budgetify.Infrastructure/"]
RUN dotnet restore "./src/Budgetify.API/./Budgetify.API.csproj"
COPY . .
WORKDIR "/src/src/Budgetify.API"
RUN dotnet build "./Budgetify.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Budgetify.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Budgetify.API.dll"]