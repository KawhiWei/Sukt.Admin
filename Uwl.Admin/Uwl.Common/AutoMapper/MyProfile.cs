using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UwlAPI.Tools.Controllers;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.MenuViewModel;
using Uwl.Data.Model.OrganizeVO;
using Uwl.Data.Model.VO.Personal;

namespace Uwl.Common.AutoMapper
{
    /// <summary>
    ///封装自己的AutoMapper继承与Profile,需要在startup里面注入MyProfile
    /// </summary>
    public class MyProfile:Profile
    {
        public MyProfile()
        {
            //菜单映射前端路由实体
            CreateMap<SysMenu, RouterBar>();
            //角色权限实体映射       指定属性映射，非相同属性自动映射
            CreateMap<SysMenu, RoleAssigMenuViewModel>().ForMember(x=>x.title,opt=>opt.MapFrom(m=>m.Name));
            //角色权限按钮实体映射
            CreateMap<SysButton, RoleAssigButtonViewModel>().ForMember(x => x.lable, opt => opt.MapFrom(m => m.Name));
            //组织机构树形实体映射
            CreateMap<SysOrganize, OrganizeViewModel>().ForMember(x => x.title, opt => opt.MapFrom(m => m.Name));




            #region 用户个人资料修改
            //个人资料修改VO到DO的映射
            CreateMap<ChangeDataVO, SysUser>();
            //CreateMap<ChangeDataVO, SysUser>().AfterMap((source, destination) =>
            //{

            //});
            #endregion

        }
    }
   
    /// <summary>
    /// 将自动映射类封装为静态属性需要映射时通过静态方法.属性
    /// </summary>
    public static class MyMappers
    {
        /// <summary>
        /// 将AutoMapper封装成一个静态类，不需要每次都去实例化
        /// </summary>
        public static IMapper ObjectMapper;
        /// <summary>
        /// 构造函数
        /// </summary>
        //public static void GetAutomapper()
        //{
        //    //AutoMapper需要先添加Nuget AutoMapper.Extensions.Microsoft.DependencyInjection包 
        //    //创建一个MyProfile类去创建映射关系，
        //    //实例化出AutoMapper对象,然后此注册MyProfile
        //    var cfgs = new MapperConfiguration(cfg => cfg.AddProfile<MyProfile>());
        //    ObjectMapper = cfgs.CreateMapper();
        //}
    }
}
