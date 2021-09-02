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