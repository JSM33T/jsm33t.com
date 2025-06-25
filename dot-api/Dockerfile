FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["JassWebApi.Base/JassWebApi.Base.csproj", "JassWebApi.Base/"]
RUN dotnet restore "JassWebApi.Base/JassWebApi.Base.csproj"
COPY . .
WORKDIR "/src/JassWebApi.Base"
RUN dotnet build "JassWebApi.Base.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "JassWebApi.Base.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JassWebApi.Base.dll"]
