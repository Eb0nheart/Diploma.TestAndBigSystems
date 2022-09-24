#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Caseopgave/Caseopgave.Api.csproj", "Caseopgave/"]
COPY ["Caseopgave.CoreFunktionalitet/Caseopgave.CoreFunktionalitet.csproj", "Caseopgave.CoreFunktionalitet/"]
RUN dotnet restore "Caseopgave/Caseopgave.Api.csproj"
COPY . .
WORKDIR "/src/Caseopgave"
RUN dotnet build "Caseopgave.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Caseopgave.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Caseopgave.Api.dll"]