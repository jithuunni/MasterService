#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MasterService.Api/MasterService.Api.csproj", "MasterService.Api/"]
RUN dotnet restore "MasterService.Api/MasterService.Api.csproj"
COPY . .
WORKDIR "/src/MasterService.Api"
RUN dotnet build "MasterService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MasterService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MasterService.Api.dll"]