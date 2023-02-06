
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProfileService.IntegrationTests/ProfileService.IntegrationTests.csproj", "ProfileService.IntegrationTests/"]
COPY ["src/ProfileService.BusinessLogic/ProfileService.BusinessLogic.csproj", "src/ProfileService.BusinessLogic/"]
COPY ["src/ProfileService.GRPC/ProfileService.GRPC.csproj", "src/ProfileService.GRPC/"]
COPY ["src/ProfileService.DataAccess/ProfileService.DataAccess.csproj", "src/ProfileService.DataAccess/"]
RUN dotnet restore "ProfileService.IntegrationTests/ProfileService.IntegrationTests.csproj"
COPY . .
WORKDIR "/src/ProfileService.IntegrationTests"
RUN dotnet build "ProfileService.IntegrationTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProfileService.IntegrationTests.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM publish AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "test", "ProfileService.IntegrationTests.dll"]