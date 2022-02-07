# Sukt 文档指南
## 亮点与优势

Sukt.Admin 是一个开箱即用的企业级权限管理应用框架。采用最新的前后端完全分离技术【 ASP.NET 6.0 】。
内部实现了 `IdentityServer4`后台管理端 ，可快速解决多客户端和多资源服务的统一认证与鉴权的问题。

### 系统架构图

![](https://wangzewei.oss-cn-beijing.aliyuncs.com/imges/系统架构图图.jpg)

### 框架功能点

> 1、丰富完整的接口文档，在查看的基础上，可以模拟前端调用，更方便。
> 2、采用多层开发，隔离性更好，封装更完善。
> 3、使用SuktCoreWebApi.Templates或SuktCoreBusinessWebApi.Templates，可以一键创建自己的Admin和其他服务项目。[IdentityServer4Admin模板]("https://www.nuget.org/packages/SuktCoreBusinessWebApi.Templates")    [基础框架模板]("https://www.nuget.org/packages/SuktCoreBusinessWebApi.Templates")
> 4、搭配代码生成器，实现快速开发，节省成本。
> 6、集成统一认证平台 `IdentityServer4` ，实现多个项目的统一认证管理，解决了之前一个项目， 一套用户的弊端，更适用微服务的开发。
> 7、丰富的接口审计日志和数据审计处理，方便线上项目快速定位异常点和数据操作问题。
> 8、支持自由切换两种数据库，SqlServer、MySql；
> 9、支持 `Docker` 容器化部署，可以搭配 k8s 更好的实现微服务。
>
> 

### 功能进度

框架模块：  

- [x] 采用`仓储+领域服务+应用+Api接口`的形式封装框架；
- [x] 异步 async/await 开发；
- [x] 支持自由切换多种数据库，MySql、SqlServer；
- [x] 实现项目启动，自动生成种子数据 ✨； 
- [x] 五种日志记录，审计/异常/请求响应/服务操作/Sql记录等； 
- [x] 支持项目事务处理（若要分布式，用cap即可）✨；
- [x] 支持服务层 AOP 切面编程 ✨；
- [x] 支持 RazorEngine.NetCore 代码模板，自动生成每层代码；
- [x] 封装SuktCoreWebApi.Templates和SuktCoreBusinessWebApi.Templates项目模板，一键重建自己的项目 ✨；
- [x] 统一集成 IdentityServer4 认证和IdentityServer4管理端 ✨;

组件模块：

- [x] 提供 Redis 做缓存处理；
- [x] 使用 Swagger 做api文档；
- [x] 使用 Automapper 处理对象映射；  
- [x] 使用MSDI做依赖注入容器，并封装服务注入 ✨；
- [x] 支持 CORS 跨域；
- [x] 使用 SeriLog 日志框架，集成原生 ILogger 接口做日志记录；
- [x] 支持 EventBus 进程内事件总线；
- [x] 支持 Redis 缓存 ✨;
- [x] 支持 MongoDB 数据层审计日志 ✨;

微服务模块：

- [x] 可配合 Docker 实现容器化；
- [x] 可配合 Jenkins 实现CI / CD；
- [x] 可配合 Consul 实现服务发现；
- [x] 可配合 Ocelot 实现网关处理；
- [x] 可配合 Nginx  实现负载均衡；
- [x] 内置 Ids4   实现认证中心和IdentityServer4Admin；

&nbsp;

### 项目使用

>1、下载项目或使用项目模板生成项目
>2、生成项目，修改appsettings.Development.json中的配置
>3、项目默认使用MySql数据库如果需要修改为SqlServer修改为【"DatabaseType":"SqlServer"】
>4、修改数据库连接字符串，这里默认使用text文本，将【ConnectionString】的文本名称修改为您对应的数据库连接字符串就可，如果报错文本文件未找到请到*****.EntityFrameworkCore中的AddSuktDbContext方法内寻找代码，
>5、MongoDB如上，如果没有MongoDB在SuktAppWebModule的特性上将MongoDB模块注释即可，
>6、以上修改完成在程序包窗口内选中Models层使用ef core迁移命令生成迁移文件，然后启动项目即可，
>7、启动项目时会判断是否需要迁移数据库、以及写入种子数据，在appsettings.Development.json中配置【"Migrations"】,默认是开启状态
>
>## 售后服务与支持  

鼓励作者，推广框架，入QQ群：980386066，随时随地解答我框架中（NetCore、Vue、DDD、IdentityServer4等）的疑难杂症。     
注意主要是帮忙解决bug和思路，不会远程授课，但是可以适当发我代码，我帮忙调试，         
