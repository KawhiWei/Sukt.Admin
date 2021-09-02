#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
RUN ls 
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Sukt.Core/src/Sukt.Core.API/Sukt.Core.API.csproj", "src/Sukt.Core.API/"]
COPY ["Sukt.Core/src/Sukt.Core.Dtos/Sukt.Core.Dtos.csproj", "src/Sukt.Core.Dtos/"]
COPY ["Sukt.Core/src/Sukt.Core.Domain.Models/Sukt.Core.Domain.Models.csproj", "src/Sukt.Core.Domain.Models/"]
COPY ["Sukt.Core/src/Sukt.Core.Identity/Sukt.Core.Identity.csproj", "src/Sukt.Core.Identity/"]
COPY ["Sukt.Core/src/Sukt.Core.Shared/Sukt.Core.Shared.csproj", "src/Sukt.Core.Shared/"]
COPY ["Sukt.Core/src/Sukt.Core.Application/Sukt.Core.Application.csproj", "src/Sukt.Core.Application/"]
COPY ["Sukt.Core/src/Sukt.Core.Domain.Services/Sukt.Core.Domain.Services.csproj", "src/Sukt.Core.Domain.Services/"]
COPY ["Sukt.Core/src/Sukt.Core.EntityFrameworkCore/Sukt.Core.EntityFrameworkCore.csproj", "src/Sukt.Core.EntityFrameworkCore/"]
RUN dotnet restore "src/Sukt.Core.API/Sukt.Core.API.csproj"
COPY . .
WORKDIR "/Sukt.Core/src/Sukt.Core.API"
RUN dotnet build "Sukt.Core.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sukt.Core.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sukt.Core.API.dll"]