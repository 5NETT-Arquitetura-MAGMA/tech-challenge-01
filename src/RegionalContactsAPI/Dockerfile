FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RegionalContactsAPI/RegionalContactsAPI.csproj", "RegionalContactsAPI/"]
COPY ["RegionalContactsAPI.Infrastructure/RegionalContactsAPI.Infrastructure.csproj", "RegionalContactsAPI.Infrastructure/"]
COPY ["RegionalContactsAPI.Core/RegionalContactsAPI.Core.csproj", "RegionalContactsAPI.Core/"]
RUN dotnet restore "RegionalContactsAPI/RegionalContactsAPI.csproj"
COPY . .
WORKDIR "/src/RegionalContactsAPI"
RUN dotnet build "RegionalContactsAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RegionalContactsAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RegionalContactsAPI.dll"]
