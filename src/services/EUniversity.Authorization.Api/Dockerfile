FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EUniversity.Authorization.Api/EUniversity.Authorization.Api.csproj", "EUniversity.Authorization.Api/"]
RUN dotnet restore "./EUniversity.Authorization.Api/EUniversity.Authorization.Api.csproj"
COPY . .
WORKDIR "/src/EUniversity.Authorization.Api"
RUN dotnet build "./EUniversity.Authorization.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EUniversity.Authorization.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EUniversity.Authorization.Api.dll"]