FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY LevelUpTgBot/LevelUpTgBot.csproj ./LevelUpTgBot/

RUN dotnet restore LevelUpTgBot/LevelUpTgBot.csproj

COPY . .

WORKDIR /src/LevelUpTgBot
RUN dotnet build LevelUpTgBot.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish LevelUpTgBot.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "LevelUpTgBot.dll"]
