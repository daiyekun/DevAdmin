#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WooDev.WebApi/WooDev.WebApi.csproj", "WooDev.WebApi/"]
RUN dotnet restore "WooDev.WebApi/WooDev.WebApi.csproj"
COPY . .
WORKDIR "/src/WooDev.WebApi"
RUN dotnet build "WooDev.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WooDev.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WooDev.WebApi.dll"]