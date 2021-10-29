using AutoMapper;
using Sukt.Core.Domain.Models;
using Sukt.Core.Domain.Models.Identity.Enum;
using Sukt.Module.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Dtos
{
    [DisplayName("用户表格Dto")]
    [AutoMap(typeof(UserEntity))]
    public class UserPageOutputDto : OutputDtoBase<Guid>
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; private set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string NormalizedUserName { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string HeadImg { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }
        /// <summary>
        /// 专业技术等级
        /// </summary>
        public string TechnicalLevel { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string Duties { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; private set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public UserTypeEnum UserType { get; private set; }
    }
}
