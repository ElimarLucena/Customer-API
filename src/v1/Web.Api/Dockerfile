#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 8080

COPY --from=pyroscope/pyroscope-dotnet:0.13.0-glibc /Pyroscope.Profiler.Native.so /dotnet/Pyroscope.Profiler.Native.so
COPY --from=pyroscope/pyroscope-dotnet:0.13.0-glibc /Pyroscope.Linux.ApiWrapper.x64.so /dotnet/Pyroscope.Linux.ApiWrapper.x64.so

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/v1/Web.Api/Web.Api.csproj", "src/v1/Web.Api/"]
COPY ["src/v1/Application/Application.csproj", "src/v1/Application/"]
COPY ["src/v1/Domain/Domain.csproj", "src/v1/Domain/"]
COPY ["src/v1/Infra.Data/Infra.Data.csproj", "src/v1/Infra.Data/"]
RUN dotnet restore "src/v1/Web.Api/Web.Api.csproj"
COPY . .
WORKDIR "/src/src/v1/Web.Api"
RUN dotnet build "Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV CORECLR_ENABLE_PROFILING=1
ENV CORECLR_PROFILER={BD1A650D-AC5D-4896-B64F-D6FA25D6B26A}
ENV CORECLR_PROFILER_PATH=/dotnet/Pyroscope.Profiler.Native.so
ENV LD_PRELOAD=/dotnet/Pyroscope.Linux.ApiWrapper.x64.so
ENV LD_LIBRARY_PATH=/dotnet
ENV PYROSCOPE_PROFILING_ENABLED=1

ENTRYPOINT ["dotnet", "Web.Api.dll"]