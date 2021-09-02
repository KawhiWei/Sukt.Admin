#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY ["src/Sukt.Core.AuthenticationCenter/Sukt.Core.AuthenticationCenter.csproj", "src/Sukt.Core.AuthenticationCenter/"]
#COPY ["src/SuktCore.MongoDB/SuktCore.MongoDB.csproj", "src/SuktCore.MongoDB/"]
#COPY ["src/Sukt.Core.Shared/SuktCore.Shared.csproj", "src/Sukt.Core.Shared/"]
#COPY ["src/Sukt.Core.Domain.Repository/Sukt.Core.Domain.Repository.csproj", "src/Sukt.Core.Domain.Repository/"]
#COPY ["src/Sukt.Core.Identity/Sukt.Core.Identity.csproj", "src/Sukt.Core.Identity/"]
#COPY ["src/Sukt.Core.EntityFrameworkCore/Sukt.Core.EntityFrameworkCore.csproj", "src/Sukt.Core.EntityFrameworkCore/"]
#COPY ["src/Sukt.Core.Domain.Models/Sukt.Core.Domain.Models.csproj", "src/Sukt.Core.Domain.Models/"]
#COPY ["src/Sukt.Core.IdentityServerFour/Sukt.Core.IdentityServerFour.csproj", "src/Sukt.Core.IdentityServerFour/"]
#COPY ["src/SuktCore.AutoMapper/SuktCore.AutoMapper.csproj", "src/SuktCore.AutoMapper/"]
#COPY ["src/Sukt.Core.IdentityServer4Store/Sukt.Core.IdentityServer4Store.csproj", "src/Sukt.Core.IdentityServer4Store/"]
#COPY ["src/Sukt.Core.MultiTenancy/Sukt.Core.MultiTenancy.csproj", "src/Sukt.Core.MultiTenancy/"]
#RUN dotnet restore "src/Sukt.Core.AuthenticationCenter/Sukt.Core.AuthenticationCenter.csproj"
COPY . .
#WORKDIR "/src/src/Sukt.Core.AuthenticationCenter"
#RUN dotnet build "Sukt.Core.AuthenticationCenter.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Sukt.Core.AuthenticationCenter.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sukt.Core.AuthenticationCenter.dll"]