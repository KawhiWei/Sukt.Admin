#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY ["src/Sukt.Core.API/Sukt.Core.API.csproj", "src/Sukt.Core.API/"]
#COPY ["src/Sukt.Core.Dtos/Sukt.Core.Dtos.csproj", "src/Sukt.Core.Dtos/"]
#COPY ["src/Sukt.Core.Shared/Sukt.Core.Shared.csproj", "src/Sukt.Core.Shared/"]
#COPY ["src/Sukt.Core.Domain.Models/Sukt.Core.Domain.Models.csproj", "src/Sukt.Core.Domain.Models/"]
#COPY ["src/Sukt.Core.Identity/Sukt.Core.Identity.csproj", "src/Sukt.Core.Identity/"]
#COPY ["src/Sukt.Core.IdentityServerFour/Sukt.Core.IdentityServerFour.csproj", "src/Sukt.Core.IdentityServerFour/"]
#COPY ["src/Sukt.Core.EntityFrameworkCore/Sukt.Core.EntityFrameworkCore.csproj", "src/Sukt.Core.EntityFrameworkCore/"]
#COPY ["src/Sukt.Core.Redis/Sukt.Core.Redis.csproj", "src/Sukt.Core.Redis/"]
#COPY ["src/Sukt.Core.Caching/Sukt.Core.Caching.csproj", "src/Sukt.Core.Caching/"]
#COPY ["src/Sukt.Core.Swagger/Sukt.Core.Swagger.csproj", "src/Sukt.Core.Swagger/"]
#COPY ["src/Sukt.Core.Aop/Sukt.Core.Aop.csproj", "src/Sukt.Core.Aop/"]
#COPY ["src/Sukt.Core.Application/Sukt.Core.Application.csproj", "src/Sukt.Core.Application/"]
#COPY ["src/Sukt.Core.MongoDB/Sukt.Core.MongoDB.csproj", "src/Sukt.Core.MongoDB/"]
#COPY ["src/Sukt.Core.Domain.Repository/Sukt.Core.Domain.Repository.csproj", "src/Sukt.Core.Domain.Repository/"]
#COPY ["src/Sukt.Core.Domain.Services/Sukt.Core.Domain.Services.csproj", "src/Sukt.Core.Domain.Services/"]
#COPY ["src/Sukt.Core.Consul/Sukt.Core.Consul.csproj", "src/Sukt.Core.Consul/"]
#COPY ["src/Sukt.Core.AutoMapper/Sukt.Core.AutoMapper.csproj", "src/Sukt.Core.AutoMapper/"]
#COPY ["src/Sukt.Core.SeriLog/Sukt.Core.SeriLog.csproj", "src/Sukt.Core.SeriLog/"]
#COPY ["src/Sukt.Core.AspNetCore/Sukt.Core.AspNetCore.csproj", "src/Sukt.Core.AspNetCore/"]
#COPY ["src/Sukt.Core.IdentityServer4Store/Sukt.Core.IdentityServer4Store.csproj", "src/Sukt.Core.IdentityServer4Store/"]
#COPY ["src/Sukt.Core.MultiTenancy/Sukt.Core.MultiTenancy.csproj", "src/Sukt.Core.MultiTenancy/"]
#RUN dotnet restore "src/Sukt.Core.API/Sukt.Core.API.csproj"
COPY . .
#WORKDIR "/src/src/Sukt.Core.API"
#RUN dotnet build "Sukt.Core.API.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Sukt.Core.API.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sukt.Core.API.dll"]