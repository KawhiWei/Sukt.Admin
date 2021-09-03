using Microsoft.AspNetCore.Http;
using Sukt.Core.Domain.Models;
using Sukt.Module.Core.Attributes.AutoMapper;
using Sukt.Module.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Core.Dtos.Identity.Role
{
    [DisplayName("角色管理新增/修改Dto")]
    [SuktAutoMapper(typeof(RoleEntity))]
    public class RoleInputDto 
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [DisplayName("角色名称")]
        public string Name { get; set; }

        /// <summary>
        /// 标准化角色名称
        /// </summary>
        [DisplayName("标准化角色名称")]
        public string NormalizedName { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        [DisplayName("是否管理员")]
        public bool IsAdmin { get; set; }
    }
    /// <summary>
    /// 角色设置权限Dto
    /// </summary>
    public class RoleMenuInputDto : InputDto<Guid>
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        [DisplayName("菜单Id")]
        public List<Guid> MenuIds { get; set; }
    }

}
