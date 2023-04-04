#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NoruBanner.Api/NoruBanner.Api.csproj", "NoruBanner.Api/"]
COPY ["NoruBanner.Infrastructure/NoruBanner.Infrastructure.csproj", "NoruBanner.Infrastructure/"]
RUN dotnet restore "NoruBanner.Api/NoruBanner.Api.csproj"
COPY . .
WORKDIR "/src/NoruBanner.Api"
RUN dotnet build "NoruBanner.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NoruBanner.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NoruBanner.Api.dll"]