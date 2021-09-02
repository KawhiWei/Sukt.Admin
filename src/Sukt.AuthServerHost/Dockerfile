#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Sukt.AuthServerHost/Sukt.AuthServerHost.csproj", "src/Sukt.AuthServerHost/"]
RUN dotnet restore "src/Sukt.AuthServerHost/Sukt.AuthServerHost.csproj"
COPY . .
WORKDIR "/src/src/Sukt.AuthServerHost"
RUN dotnet build "Sukt.AuthServerHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sukt.AuthServerHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sukt.AuthServerHost.dll"]