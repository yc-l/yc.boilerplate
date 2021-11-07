# YC.Boilerplate

<p align="center">
    <a href="README.md">English</a> |  
    <span>中文</a>
</p>

## 框架介绍

YC.Boilerplate 是一套快速开发框架，采用当下流行的前后端分离开发模式，前端 采用VUE 2.0、后端采用Net 5.0；框架实现了多租户、动态webApi、多种ORM、IOC、数据库表和业务代码生成等等一系列模块，并开发了用户管理、角色权限、组织机构、数据字典、审计日志等常规功能。

框架的宗旨:构建一套松耦合、灵活组合、功能齐全、开发便捷、减少开发工作量的样板化开发框架。

元磁之力框架技术群QQ：1060819005

## 最新迭代

1. 集成docker 相关
2. 内置docker 容器监测工具
3. 群资料中有相关打包镜像
4. 提供镜像部署操作教程【见doc目录】


## 视频教程

- 元磁之力框架开源初心和框架设计介绍（上）： <https://www.bilibili.com/video/BV1VM4y1G7hC/>
- 元磁之力框架开源初心和框架设计介绍（下）： <https://www.bilibili.com/video/BV15h411s7w6/>
- 元磁之力框架数据库表和代码生成使用教程实战： <https://www.bilibili.com/video/BV1oM4y137D5/>

## 快速入门

1. [在线演示](http://yc.yc-l.com/)
2. [在线文档](http://doc.yc-l.com/)
3. 常规前后端分离框架：前端：src\Front\yc.webfront ，后端：src\Backstage\YC.Boilerplate.sln 
4. 框架微服务版本见：src\MicroService\YC.MicroService.sln
5. 框架最新版本已经引入 ElasticSearch 等大数据套件，实现千万级别以上的分布式检索、统计、分析。详细请看最新的文档。
6. 初始化数据库：src/Db (租户1 和租户2 分库演示数据)


## 框架技术栈和开发模式

- 开发模式：当下流行的前后端分离。
- 前端：vue2.0、elementUI等技术栈，为单页面应用程序提供了项目模板。
- 后端采用NET 5.0。

#### 框架特点

- 模块化开发，灵活组合，将解耦进行到底，按需注入使用。
- 当下流行的开发模式，分层明确。
- 配合框架自带代码生成器快速完成CRUD和树形等复杂功能逻辑实现，不敲一行代码，节省60%工作量。

#### YC.Boilerplate 框架分层介绍

> 常规版本框架分层

- 核心层：```YC.Core``` 主要实现顶层设计类接口规范和一些类的封装，比如：租户接口、AopInterceptor AOP 拦截注入、TokenContext token逻辑、公共特性等等。
- 数据层：```YC.DapperFrameWork```、```YC.FreeSqlFrameWork``` 多项ORM 封装，主要存在仓储、工作单元等。
- 公共层：```YC.Common``` 主要是各种公共类的使用。
- 业务服务层：```YC.ApplicationService``` 业务逻辑实现，默认实现动态webapi。
- 实体层:```YC.Model``` 常规的数据表实体，枚举等，以及常规的model需要用的基础方法。
- 业务Api层：```YC.ServiceWebApi``` 提供对外接口服务启动项目。

> 微服务版本框架分层

- 聚合服务层：```YC.Micro.AggregateServiceWebApi```,多种服务融合调用。
- 公共常用配置层：```YC.Micro.Configuration``` 公共常用配置层。
- 各个独立服务：```YC.Micro.xxxxWebService``` 指定服务。
- 其他：注册、熔断、负载均衡、日志运维、身份认证中心、容器化等,敬请期待。

> 模块层（按需使用）

- 数据库表和代码生成模块：```YC.CodeGenerate```
- Redis 缓存模块：```YC.Cache.Redis```
- 动态WebApi模块：```YC.Core.DynamicApi```
- 日志模块：```YC.Log.Serilog```
- 定时服务 Quartz.net ```YC.QuartzService```
- Mongodb 模块 ```YC.MongoDB```
- 图数据库Neo4j ```YC.Neo4j```
- 大数据套件ElasticSearch ```YC.ElasticSearch```
  
> 单元测试

单元测试：```UnitTestProject```
数据库表和代码生成测试：```YC.CodeGenerateTest```
其他各个模块配套的单元测试:```YC.QuartzTest```、```YC.MongoDbXUnitTest```、```YC.Neo4jXUnitTest```、```YC.ElasticSearchXUnitTest```

#### 框架功能模块

![image](https://gitee.com/linxuanming/yc.boilerplate/raw/master/assets/images/%E6%A1%86%E6%9E%B6%E5%9B%BE.png)

#### 框架特性

1. 基于最新的.NET技术 NET 5.0。
2. 实现领域驱动设计（实体、仓储、领域服务、领域事件、应用服务、数据传输对象，工作单元等等）。
3. 实现分层体系结构（领域层，应用层，展现层和基础设施层）。
4. 提供了一个基础架构来开发可重用可配置的模块。
5. 集成一些最流行的开源框架/库，也许有些是你正在使用的。
6. 提供了一个基础架构让我们很方便地使用依赖注入（使用Autofac作为依赖注入的容器）。
7. 提供Repository仓储模式支持不同的ORM（已实现dapperFramework 、freesqlFrameWork、Redis等）。
8. 支持并实现数据库迁移（采用自由映射Table ）。
9. 模块化开发（每个模块有独立的采用autofac模块注入形式，根据不同业务可以动态切换指定数据库）。
10. 统一的异常处理（应用层几乎不需要处理自己写异常处理代码）。
11. 通过Application Services自动创建Web Api层（不需要写ApiController层了）。
12. 提供基类和帮助类让我们方便地实现一些常见的任务。
13. 使用“约定优于配置原则”。
14. 实现多租户，按照不同的租户分库服务端。
15. 实现了基于表模型生成对应的全流程代码，包括：model、DTO、Service、前端展示界面（常规CRUD和树形功能）、路由规则、Mapper model和Dto映射等代码。
16. 框架已经实现常规基础功能，如：身份验证用户&角色管理、系统设置、存取管理（系统级、租户级、用户级，作用范围自动管理）、审计日志（自动记录每一次接口的调用者和参数）、组织机构等，实现框架开箱即用。
17. 框架采用redis 作为cache和session 存储，脱离cookie 使用，解决非web的等也可以使用框架进行多样的场景需求改造。

## 打赏支持

<img src="https://gitee.com/linxuanming/yc.boilerplate/raw/master/assets/images/payCode/weixin_CollectionCode.jpg" width="36%" height="36%">
<img src="https://gitee.com/linxuanming/yc.boilerplate/raw/master/assets/images/payCode/alipay_CollectionCode.jpg" width="36%" height="36%">

捐赠人 | 捐赠金额（元）
---|---
小蚂蚁| 66.66
张三家的猫 | 100
刘茜 | 99
ak11 | 8.8
弗拉门 | 200



## 参与贡献

1. Fork 本仓库
2. 新建 Feat_xxx 分支
3. 提交代码
4. 新建 Pull Request


