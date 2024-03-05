FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EUniversity.Manager.Api/EUniversity.Manager.Api.csproj", "EUniversity.Manager.Api/"]
RUN dotnet restore "./EUniversity.Manager.Api/EUniversity.Manager.Api.csproj"
COPY . .
WORKDIR "/src/EUniversity.Manager.Api"
RUN dotnet build "./EUniversity.Manager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EUniversity.Manager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EUniversity.Manager.Api.dll"]