## 项目代号：Sukt.Core  苏克
[Asp.NetCore 手册
<----基础学习请参考
https://windsting.github.io/little-aspnetcore-book/book/                   
https://www.cnblogs.com/laozhang-is-phi/
---->]

###主要技术栈
+仓储层/服务层/接口

+CRUD
	+EntityFrameworkCore ORM框架
+DI
	+.Net Core官方DI
+缓存
	+ Redis缓存技术


## 功能目录及阶段目标

### 阶段一
-[x]001 实现Sukt.Core的分层搭建
-[x]002 实现.NetCore 官方DI批量注入、AOP切面等一些功能
-[x]003 实现AutoMapper模块化,不需要再Startup中进行配置
-[x]004 实现Swagger模块化,不需要再Startup中进行配置
-[x]005 实现应用服务层注入不需要手动注入,在应用服务层中配置特性进行实现
-[x]006 实现仓储层/调通数据库/工作单元
-[x]007 实现用户、角色、菜单、组织架构等功能







＃Uwl.Admin.Core
本项目是基于.Net Core3.x开发的一个开源后台管理框架

特别感谢老张\ DX \ 大黄瓜 \残云等给帮助

再次特推一下老张的博客（https://www.cnblogs.com/laozhang-is-phi/）

感谢各位的支持Uwl.Admin.Core只是一个基础的权限角色管理项目

Uwl.Admin.Core 框架是基于.Net Core2.2开发的一个开源后台管理框架目前有以下模块
组织机构、菜单管理、按钮管理、用户管理、部门管理、角色管理、用户角色、角色权限、任务计划调度。

希望广大码友提出更多功能我来完善。

很多地方可能设计的不够好我的想法也比较简单希望各位朋友能一起加入维护的行列（特别@老张，我要拖你下水）

项目结构：
 Uwl.Attribute               自定义特性存放
 Uwl.Cache                   缓存
 Uwl.Common                  公共接口定义
 Uwl.Data.EntityFramework    仓储实现层
 Uwl.Data.Model              实体模型
 Uwl.Data.Server             业务逻辑服务层
 Uwl.Domain                  仓储接口定义
 Uwl.Extends                 自定义扩展
 Uwl.QuartzNet.JobCenter     基于Quartz.Net的任务计划调度中心
 UwlAPI.Tools                API接口开放

使用技术：
  .Net Core 3.x API（因为想单纯建造前替换分离，因此就选择的API）
  Swagger前瞻性文档说明，基于RESTful风格编写接口
  仓库+服务仓储模式编程
  异步和等待初步编程
  核心微软官方自带DI依赖注入框架
  JWT权限验证
数据库技术
  Entity Framework Core3.x
  AutoMapper自动对象映射
分布式缓存技术
  Redis轻量级分布式缓存(暂未完成)

本项目开源也是为.Net社区做一份贡献，希望大家多多为社区做贡献.

