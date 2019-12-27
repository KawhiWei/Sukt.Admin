＃Uwl.Admin.Core
本项目是基于.Net Core3.1开发的一个开源后台管理框架

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
  .Net Core 2.x API（因为想单纯建造前替换分离，因此就选择的API）
  Swagger前瞻性文档说明，基于RESTful风格编写接口
  仓库+服务仓储模式编程
  异步和等待初步编程
  核心微软官方自带DI依赖注入框架
  JWT权限验证
数据库技术
  Entity Framework Core2.x
  AutoMapper自动对象映射
分布式缓存技术
  Redis轻量级分布式缓存(暂未完成)

本项目开源也是为.Net社区做一份贡献，希望大家多多为社区做贡献.
 
