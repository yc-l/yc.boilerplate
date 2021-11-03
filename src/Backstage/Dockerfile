#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Dockerfile 的指令每执行一次都会在 docker 上新建一层。所以过多无意义的层，会造成镜像膨胀过大，
#可以使用RUN 将命令&& 拼接只打包一层镜像

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

WORKDIR /app
EXPOSE 8001

#基于 aspnet:5.0
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BasicAppLayer/YC.ServiceWebApi/YC.ServiceWebApi.csproj", "BasicAppLayer/YC.ServiceWebApi/"]
COPY ["Module/YC.MongoDB/YC.MongoDB.csproj", "Module/YC.MongoDB/"]
COPY ["Module/YC.QuartzService/YC.QuartzService.csproj", "Module/YC.QuartzService/"]
COPY ["BasicLayer/YC.DapperFrameWork/YC.DapperFrameWork.csproj", "BasicLayer/YC.DapperFrameWork/"]
COPY ["BasicLayer/YC.Core/YC.Core.csproj", "BasicLayer/YC.Core/"]
COPY ["BasicLayer/YC.Common/YC.Common.csproj", "BasicLayer/YC.Common/"]
COPY ["BasicAppLayer/YC.Model/YC.Model.csproj", "BasicAppLayer/YC.Model/"]
COPY ["BasicLayer/YC.FreeSqlFrameWork/YC.FreeSqlFrameWork.csproj", "BasicLayer/YC.FreeSqlFrameWork/"]
COPY ["BasicAppLayer/YC.ApplicationService/YC.ApplicationService.csproj", "BasicAppLayer/YC.ApplicationService/"]
COPY ["Module/YC.Cache.Redis/YC.Cache.Redis.csproj", "Module/YC.Cache.Redis/"]
COPY ["Module/YC.ElasticSearch/YC.ElasticSearch.csproj", "Module/YC.ElasticSearch/"]
COPY ["Module/YC.Core.DynamicApi/YC.Core.DynamicApi.csproj", "Module/YC.Core.DynamicApi/"]
COPY ["Module/YC.Log.Serilog/YC.Log.Serilog.csproj", "Module/YC.Log.Serilog/"]


RUN dotnet restore "BasicAppLayer/YC.ServiceWebApi/YC.ServiceWebApi.csproj"
COPY . .
WORKDIR "/src/BasicAppLayer/YC.ServiceWebApi"

RUN dotnet build "YC.ServiceWebApi.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "YC.ServiceWebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
#将publish 执行步骤中/app/publish 目录下文件全部 copy  . 当前目录下【WORKDIR /app 当前目录】
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YC.ServiceWebApi.dll","--urls","http://*:8001"]