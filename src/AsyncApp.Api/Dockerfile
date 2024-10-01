FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080

# Строим приложение
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["src/AsyncApp.Api/AsyncApp.Api.csproj", "AsyncApp.Api/"]
RUN dotnet restore "AsyncApp.Api/AsyncApp.Api.csproj"
COPY ./src/AsyncApp.Api/. ./AsyncApp.Api/
WORKDIR "/src/AsyncApp.Api"
RUN dotnet build "AsyncApp.Api.csproj" -c Release -o /app/build

# Публикуем
FROM build AS publish
RUN dotnet publish "AsyncApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "AsyncApp.Api.dll"]
