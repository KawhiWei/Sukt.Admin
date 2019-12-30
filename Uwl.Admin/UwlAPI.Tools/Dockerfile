#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["UwlAPI.Tools/UwlAPI.Tools.csproj", "UwlAPI.Tools/"]
COPY ["Uwl.Data.Server/Uwl.Data.Server.csproj", "Uwl.Data.Server/"]
COPY ["Uwl.Extends/Uwl.Extends.csproj", "Uwl.Extends/"]
COPY ["Uwl.Attribute/Uwl.Attribute.csproj", "Uwl.Attribute/"]
COPY ["Uwl.Data.Model/Uwl.Data.Model.csproj", "Uwl.Data.Model/"]
COPY ["Uwl.Domain/Uwl.Domain.csproj", "Uwl.Domain/"]
COPY ["Uwl.Common/Uwl.Common.csproj", "Uwl.Common/"]
COPY ["Uwl.QuartzNet.JobCenter/Uwl.QuartzNet.JobCenter.csproj", "Uwl.QuartzNet.JobCenter/"]
COPY ["Uwl.Data.EntityFramework/Uwl.Data.EntityFramework.csproj", "Uwl.Data.EntityFramework/"]
COPY ["Uwl.ScheduledTask.Job/Uwl.ScheduledTask.Job.csproj", "Uwl.ScheduledTask.Job/"]
RUN dotnet restore "UwlAPI.Tools/UwlAPI.Tools.csproj"
COPY . .
WORKDIR "/src/UwlAPI.Tools"
RUN dotnet build "UwlAPI.Tools.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UwlAPI.Tools.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UwlAPI.Tools.dll"]