#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Backstage/BasicAppLayer/YC.ServiceWebApi/YC.ServiceWebApi.csproj", "src/Backstage/BasicAppLayer/YC.ServiceWebApi/"]
COPY ["src/Backstage/BasicLayer/YC.DapperFrameWork/YC.DapperFrameWork.csproj", "src/Backstage/BasicLayer/YC.DapperFrameWork/"]
COPY ["src/Backstage/BasicLayer/YC.Core/YC.Core.csproj", "src/Backstage/BasicLayer/YC.Core/"]
COPY ["src/Backstage/BasicLayer/YC.Common/YC.Common.csproj", "src/Backstage/BasicLayer/YC.Common/"]
COPY ["src/Backstage/Module/YC.Log.Serilog/YC.Log.Serilog.csproj", "src/Backstage/Module/YC.Log.Serilog/"]
COPY ["src/Backstage/BasicAppLayer/YC.ApplicationService/YC.ApplicationService.csproj", "src/Backstage/BasicAppLayer/YC.ApplicationService/"]
COPY ["src/Backstage/BasicAppLayer/YC.Model/YC.Model.csproj", "src/Backstage/BasicAppLayer/YC.Model/"]
COPY ["src/Backstage/BasicLayer/YC.FreeSqlFrameWork/YC.FreeSqlFrameWork.csproj", "src/Backstage/BasicLayer/YC.FreeSqlFrameWork/"]
COPY ["src/Backstage/Module/YC.Cache.Redis/YC.Cache.Redis.csproj", "src/Backstage/Module/YC.Cache.Redis/"]
COPY ["src/Backstage/Module/YC.Core.DynamicApi/YC.Core.DynamicApi.csproj", "src/Backstage/Module/YC.Core.DynamicApi/"]
RUN dotnet restore "src/Backstage/BasicAppLayer/YC.ServiceWebApi/YC.ServiceWebApi.csproj"
COPY . .
WORKDIR "/src/src/Backstage/BasicAppLayer/YC.ServiceWebApi"
RUN dotnet build "YC.ServiceWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YC.ServiceWebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YC.ServiceWebApi.dll"]