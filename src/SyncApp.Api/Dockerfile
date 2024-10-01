FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080

# Строим приложение
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["src/SyncApp.Api/SyncApp.Api.csproj", "SyncApp.Api/"]
RUN dotnet restore "SyncApp.Api/SyncApp.Api.csproj"
COPY ./src/SyncApp.Api/. ./SyncApp.Api/
WORKDIR "/src/SyncApp.Api"
RUN dotnet build "SyncApp.Api.csproj" -c Release -o /app/build

# Публикуем
FROM build AS publish
RUN dotnet publish "SyncApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "SyncApp.Api.dll"]
