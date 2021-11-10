FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV TZ=Asia/Shanghai
EXPOSE 80
COPY . .
ENTRYPOINT ["dotnet", "Sukt.Core.API.dll"]