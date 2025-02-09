#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/v1/Web.Api/Web.Api.csproj", "src/v1/Web.Api/"]
COPY ["src/v1/Application/Application.csproj", "src/v1/Application/"]
COPY ["src/v1/Domain/Domain.csproj", "src/v1/Domain/"]
COPY ["src/v1/Infra.Data/Infra.Data.csproj", "src/v1/Infra.Data/"]
COPY ["tests/IntegrationTests/IntegrationTests.csproj", "tests/IntegrationTests/"]
RUN dotnet restore "src/v1/Web.Api/Web.Api.csproj"
COPY . .
WORKDIR "/src/src/v1/Web.Api"
RUN dotnet build "Web.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Api.dll"]