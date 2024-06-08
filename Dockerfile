FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./CV/CV.csproj", "./"]
COPY ["./Common/Common.csproj", "../Common/"]
COPY ["./WebAPI/WebAPI.csproj", "../WebAPI/"]
COPY ["./DataAccess/DataAccess.csproj", "../DataAccess/"]
RUN dotnet restore "CV.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "./CV/CV.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CV/CV.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CV.dll"]
