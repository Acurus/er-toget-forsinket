FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["er-toget-forsinket-api/er-toget-forsinket-api.csproj", "er-toget-forsinket-api/"]
RUN dotnet restore "er-toget-forsinket-api/er-toget-forsinket-api.csproj"
COPY . .
WORKDIR "/src/er-toget-forsinket-api"
RUN dotnet build "./er-toget-forsinket-api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./er-toget-forsinket-api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "er-toget-forsinket-api.dll"]
