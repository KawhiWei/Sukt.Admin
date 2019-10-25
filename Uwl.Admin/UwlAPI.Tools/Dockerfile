FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["UwlAPI.Tools/UwlAPI.Tools.csproj", "UwlAPI.Tools/"]
COPY ["Uwl.Data.Server/Uwl.Data.Server.csproj", "Uwl.Data.Server/"]
COPY ["Uwl.Extends/Uwl.Extends.csproj", "Uwl.Extends/"]
COPY ["Uwl.Data.EntityFramework/Uwl.Data.EntityFramework.csproj", "Uwl.Data.EntityFramework/"]
COPY ["Uwl.Data.Model/Uwl.Data.Model.csproj", "Uwl.Data.Model/"]
COPY ["Uwl.Attribute/Uwl.Attribute.csproj", "Uwl.Attribute/"]
COPY ["Uwl.Domain/Uwl.Domain.csproj", "Uwl.Domain/"]
COPY ["Uwl.Common/Uwl.Common.csproj", "Uwl.Common/"]
COPY ["Uwl.Cache/Uwl.Cache.csproj", "Uwl.Cache/"]
RUN dotnet restore "UwlAPI.Tools/UwlAPI.Tools.csproj"
COPY . .
WORKDIR "/src/UwlAPI.Tools"
RUN dotnet build "UwlAPI.Tools.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UwlAPI.Tools.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UwlAPI.Tools.dll"]