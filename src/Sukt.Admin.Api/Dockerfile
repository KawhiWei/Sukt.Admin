#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["src/Sukt.Admin.Api/Sukt.Admin.Api.csproj", "src/Sukt.Admin.Api/"]
COPY ["src/Sukt.Admin.Application/Sukt.Admin.Application.csproj", "src/Sukt.Admin.Application/"]
COPY ["src/Sukt.Admin.Domain/Sukt.Admin.Domain.csproj", "src/Sukt.Admin.Domain/"]
COPY ["src/Sukt.Admin.Dto/Sukt.Admin.Dto.csproj", "src/Sukt.Admin.Dto/"]
COPY ["src/Sukt.Admin.Query/Sukt.Admin.Query.csproj", "src/Sukt.Admin.Query/"]
COPY ["src/Sukt.Admin.EntityFrameworkCore/Sukt.Admin.EntityFrameworkCore.csproj", "src/Sukt.Admin.EntityFrameworkCore/"]
RUN dotnet restore "src/Sukt.Admin.Api/Sukt.Admin.Api.csproj"
COPY . .
WORKDIR "/src/src/Sukt.Admin.Api"
RUN dotnet build "Sukt.Admin.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sukt.Admin.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENV ASPNETCORE_HOSTINGSTARTUPASSEMBLIES="SkyAPM.Agent.AspNetCore" 在启动容器的时候需要通过映射的方式传入进来，卸载dockerfile中无效
ENTRYPOINT ["dotnet", "Sukt.Admin.Api.dll"]