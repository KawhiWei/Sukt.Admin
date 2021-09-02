using AutoMapper;
using Sukt.Core.Domain.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IDN.Services.BasicsService.Dtos.Identity.User
{
    [DisplayName("用户编辑查看加载模型")]
    [AutoMap(typeof(UserEntity))]
    public class UserLoadFormOutputDto
    {
        public UserLoadFormOutputDto()
        {

        }

        /// <summary>
        /// 用户名称
        /// </summary>
        [DisplayName("用户名称")]
        public string UserName { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        [DisplayName("登录账号")]
        public string NormalizedUserName { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [DisplayName("电子邮箱"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        [DisplayName("用户头像")]
        public string HeadImg { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [DisplayName("生日")]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        [DisplayName("学历")]
        public string Education { get; set; }

        /// <summary>
        /// 专业技术等级
        /// </summary>
        [DisplayName("专业技术等级")]
        public string TechnicalLevel { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [DisplayName("身份证号")]
        public string IdCard { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        [DisplayName("职务")]
        public string Duties { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [DisplayName("部门")]
        public string Department { get; set; }
    }
}
