FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RedisWeb.API/RedisWeb.API.csproj", "RedisWeb.API/"]
RUN dotnet restore "RedisWeb.API/RedisWeb.API.csproj"
COPY . .
WORKDIR "/src/RedisWeb.API"
RUN dotnet build "RedisWeb.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedisWeb.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedisWeb.API.dll"]
