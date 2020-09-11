using Sukt.Core.Domain.Models;
using Sukt.Core.Shared.Attributes.AutoMapper;
using Sukt.Core.Shared.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sukt.Core.Dtos.Identity.Role
{
    [DisplayName("角色管理新增/修改Dto")]
    [SuktAutoMapper(typeof(RoleEntity))]
    public class RoleInputDto:InputDto<Guid>
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
        /// <summary>
        /// 菜单Id
        /// </summary>
        [DisplayName("菜单Id")]
        public List<Guid> MenuIds { get; set; }

    }
}
