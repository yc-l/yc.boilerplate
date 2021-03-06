
<p align="center">
    <img height="260" src="https://github.com/yc-l/yc.boilerplate/blob/master/assets/images/logo1.png">
</p>



<p align="center">
    <a href="README.zh.md">中文</a> |  
    <span>English</a>
</p>

    
![GitHub stars](https://img.shields.io/github/stars/yc-l/yc.boilerplate)
![GitHub fork](https://img.shields.io/github/forks/yc-l/yc.boilerplate?style=flat-square&label=Forks&logo=github)
![Commit Date](https://img.shields.io/github/last-commit/yc-l/yc.boilerplate/master.svg?logo=github&logoColor=green&label=commit)
![GitHub license](https://img.shields.io/github/license/yc-l/yc.boilerplate)



## Framework introduction

YC.Boilerplate is a set of rapid development framework, which adopts the current popular front-end and back-end separation development mode, with Vue 2.0 in the front end and net 5.0 in the back end; The framework implements Multi tenant, dynamic webapi, multiple ORM, IOC, database table and business code generation, and developed user management, role permissions, organization
General functions such as data dictionary and audit log.

The purpose of the framework is to build a model development framework with loose coupling, flexible combination, complete functions, convenient development and reducing development workload.

YC.boilerplate framework technology group QQ: 1060819005


## Latest iteration

1. Integrate docker related
2. Built in docker container monitoring tool
3. There are relevant packaged images in the group data
4. Provide image deployment operation tutorial [see doc directory]
5. New micro services: Ocelot gateway, consumer, load balancing, fuse, identityserver 4 identity authentication
   
## Video tutorial
- Introduction to the original center and frame design of yuanci force frame (Part I):< https://www.bilibili.com/video/BV1VM4y1G7hC/ >
- Introduction to the original center and frame design of yuanci force frame (Part 2):< https://www.bilibili.com/video/BV15h411s7w6/ >
- Meta magnetic force framework database table and code generation tutorial practice:< https://www.bilibili.com/video/BV1oM4y137D5/ >
- Introduction to microservice version experience：<https://www.bilibili.com/video/BV1X44y1a7xU?spm_id_from=333.999.0.0>
  
## Quick start
1. [Online demonstration]（ http://yc.yc-l.com/ )
2. [online document]（ http://doc.yc-l.com/ )
3. Conventional front and rear end separation frame: front end: src\front\yc.webfront, back end: src\backstage\yc.boilerplate.sln
4. See src\microservice\yc.microservice.sln for the framework microservice version
5. The latest version of the framework has introduced elasticsearch and other big data suites to realize distributed retrieval, statistics and analysis of more than ten million levels. Please see the latest documentation for details.
6. Initialize the database: src\DB (tenant 1 and tenant 2 sub database presentation data)


## Framework technology stack and development mode
- Development mode: the current popular front and rear end separation.
- Front end: vue2.0, elementui and other technology stacks, which provide project templates for single page applications.
- The back end adopts net 5.0.

#### Frame features
- Modular development, flexible combination, decoupling to the end, injection and use on demand.
- The current popular development model has clear layers.
- Cooperate with the built-in code generator of the framework to quickly complete the logic implementation of complex functions such as crud and tree without typing a line of code, saving 60% of the workload.

#### YC. Boilerplate framework layered introduction

>General version framework hierarchy

- Core layer: ```YC.Core ``` mainly implements the top-level design class interface specification and encapsulation of some classes, such as tenant interface, aopinterceptor AOP interception injection, tokencontext token logic, public features, etc.
- Data layer: ```YC.Dapperframework ```, ``` YC.Freesqlframework ``` multiple ORM packages, mainly including storage, work unit, etc.
- Public layer: ``` YC.Common ``` is mainly used by various public classes.
- Business service layer: ``` YC.Applicationservice ``` business logic implementation, which implements dynamic webapi by default.
- Entity layer: ``` YC.model ``` regular data tables, entities, enumerations, etc., as well as the basic methods required by the regular model.
- Business API layer: ```YC.Servicewebapi ``` provides external interface service startup project.

>Microservice version framework layering

- Aggregation service layer: ``` YC.Micro.Aggregateservicewebapi ```, multiple service fusion calls.
- Common configuration layer: ```YC.Micro.Configuration ``` common configuration layer.
- Independent services: ```YC.Micro.Xxxwebservice ``` specify the service.
- Others: registration, fusing, load balancing, log operation and maintenance, identity authentication center, containerization, etc. Please look forward to it.
- Other combinations: configuration center, consumer, load balancing, loadbalance, identity authentication center ids4, container docker.
- Please look forward to log operation and maintenance, distributed transactions, etc.
  
![image](https://github.com/yc-l/yc.boilerplate/blob/master/assets/images/YC.Micro%20%E5%BE%AE%E6%9C%8D%E5%8A%A1.jpg)

>Module layer (on demand)
- Database table and code generation module: ```YC.codegenerate```
- Redis cache module: ```YC.cache.redis```
- Dynamic webapi module: ```YC.core.dynamicapi```
- Log module: ```YC.log.serial```
- Timing service quartz.net ```YC.quartzservice```
- Mongodb module ```YC.mongodb```
- Figure database neo4j ``` YC.neo4j```
-Big data suite elasticsearch ``` YC.elasticsearch```

>Unit test

Unit test: ```unittestproject```
Database table and code generation test:
- ```yc.codegeneratetest```
Unit tests for other modules: 
- ```YC.Quartztest ```
- ```YC.Mongodbxunittest ```
- ```YC.Neo4jxunittest ```
- ```YC.Elasticsearchxunittest ```

#### FrameWork function module

![image](https://github.com/yc-l/yc.boilerplate/blob/master/assets/images/%E6%A1%86%E6%9E%B6%E5%9B%BE.png)

#### FrameWork properties
1. Based on the latest. Net technology net 5.0.
2. Implement Domain Driven Design (entity, warehouse, domain service, domain event, application service, data transmission object, work unit, etc.).
3. Implement layered architecture (domain layer, application layer, presentation layer and infrastructure layer).
4. Provide an infrastructure to develop reusable and configurable modules.
5. Integrate some of the most popular open source frameworks / libraries, perhaps some of which you are using.
6. An infrastructure is provided to facilitate the use of dependency injection (using Autofac as the container for dependency injection).
7. Provide repository storage mode to support different ORM (dapper framework, freesqlframework, redis, etc. have been implemented).
8. Support and implement database migration (using free mapping table).
9. Modular development (each module has its own, which adopts the form of Autofac module injection, and can dynamically switch the specified database according to different services).
10. Unified exception handling (the application layer hardly needs to write its own exception handling code).
11. Automatically create the web API layer through application services (there is no need to write the apicontroller layer).
12. Providing base classes and help classes allows us to easily implement some common tasks.
13. Use "agreement over configuration principle".
14. Realize multi tenancy, and divide the server according to different tenants.
15. The corresponding whole process code is generated based on the table model, including model, dto, service, front-end display interface (conventional crud and tree functions), routing rules, mapper model and dto mapping.
16. The framework has realized conventional basic functions, such as authentication user &amp; role management, system setting, access management (system level, tenant level, user level, automatic scope management), audit log (automatically recording the callers and parameters of each interface), organization, etc., so as to realize the out of the box use of the framework.
17. The framework uses redis as cache and session storage, which is separated from cookies to solve non web problems. The framework can also be used to transform various scene requirements.

## Reward support

<img src="https://github.com/yc-l/yc.boilerplate/blob/master/assets/images/payCode/weixin_CollectionCode.jpg" width="36%" height="36%">
<img src="https://github.com/yc-l/yc.boilerplate/blob/master/assets/images/payCode/alipay_CollectionCode.jpg" width="36%" height="36%">


Donor | Donation amount (¥)
---|---
小蚂蚁| 66.66
张三家的猫 | 100
刘茜 | 99
ak11 | 8.8
弗拉门 | 200

## Participation contribution
1. Fork warehouse
2. New feat_ XXX branch
3. Submission code
4. Create a new pull request


