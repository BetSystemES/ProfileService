
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProfileService.FunctionalTests/ProfileService.FunctionalTests.csproj", "ProfileService.FunctionalTests/"]
COPY ["src/ProfileService.GRPC/ProfileService.GRPC.csproj", "src/ProfileService.GRPC/"]
COPY ["src/ProfileService.DataAccess/ProfileService.DataAccess.csproj", "src/ProfileService.DataAccess/"]
COPY ["src/ProfileService.BusinessLogic/ProfileService.BusinessLogic.csproj", "src/ProfileService.BusinessLogic/"]
RUN dotnet restore "ProfileService.FunctionalTests/ProfileService.FunctionalTests.csproj"
COPY . .
WORKDIR "/src/ProfileService.FunctionalTests"
RUN dotnet build "ProfileService.FunctionalTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProfileService.FunctionalTests.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM publish AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "test", "ProfileService.FunctionalTests.dll"]