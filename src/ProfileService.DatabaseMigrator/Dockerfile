#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ProfileService.DatabaseMigrator/ProfileService.DatabaseMigrator.csproj", "src/ProfileService.DatabaseMigrator/"]
COPY ["src/ProfileService.DataAccess/ProfileService.DataAccess.csproj", "src/ProfileService.DataAccess/"]
COPY ["src/ProfileService.BusinessLogic/ProfileService.BusinessLogic.csproj", "src/ProfileService.BusinessLogic/"]
RUN dotnet restore "src/ProfileService.DatabaseMigrator/ProfileService.DatabaseMigrator.csproj"
COPY . .
WORKDIR "/src/src/ProfileService.DatabaseMigrator"
RUN dotnet build "ProfileService.DatabaseMigrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProfileService.DatabaseMigrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .