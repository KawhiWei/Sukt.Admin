using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Data.EntityFramework.Uwl_DbContext;
using Microsoft.Extensions.DependencyInjection;
using Uwl.Data.Model.BaseModel;

namespace Uwl.Data.EntityFramework
{
    public static class SeedData
    {
        ////public static void InsertData(IServiceProvider serviceProvider)
        ////{
        ////    using (var context = new UwlDbContext(serviceProvider.GetRequiredService<DbContextOptions>(UwlDbContext)))
        ////    {
        ////        Guid deptmenguid = Guid.NewGuid();
        ////        context.Departments.Add(new Department
        ////        {
        ////            Id = deptmenguid,
        ////            Name = "开发技术部",
        ////            ParentId = Guid.Empty
        ////        });
        ////        context.Users.Add(new User
        ////        {
        ////            UserName = "admin",
        ////            Password = "123456",
        ////            Name = "超级管理员",
        ////            DeptmentId = deptmenguid
        ////        });
        ////        context.Menus.AddRange(
        ////            new Menu
        ////            {
        ////                Name = "组织机构管理",
        ////                Code = "Department",
        ////                SerialNumber = 0,
        ////                ParentId = Guid.Empty,
        ////                Icon = "fa fa-link"
        ////            },
        ////            new Menu
        ////            {
        ////                Name = "角色管理",
        ////                Code = "Role",
        ////                SerialNumber = 1,
        ////                ParentId = Guid.Empty,
        ////                Icon = "fa fa-link"
        ////            },
        ////            new Menu
        ////            {
        ////                Name = "用户管理",
        ////                Code = "User",
        ////                SerialNumber = 2,
        ////                ParentId = Guid.Empty,
        ////                Icon = "fa fa-link"
        ////            },
        ////            new Menu
        ////            {
        ////                Name = "功能管理",
        ////                Code = "Department",
        ////                SerialNumber = 3,
        ////                ParentId = Guid.Empty,
        ////                Icon = "fa fa-link"
        ////            }
        ////            );
        ////        context.SaveChanges();
        ////    }
        ////}
    }
}
