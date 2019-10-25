using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uwl.Attribute.ExcelAttribute;
using Uwl.Data.Model.Enum;

namespace Uwl.Data.Model.BaseModel
{
    [Serializable]
    public class SysUser:Entity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [ExcelColumnName("姓名",ColumnWith =30,Sort =1)]
        [ExcelReadColumnName("姓名")]
        public string Name { get; set; }
        /// <summary>
        /// 性别true=男。false=女
        /// </summary>
        public bool Sex { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [ExcelColumnName("邮箱", ColumnWith = 30, Sort = 2)]
        [ExcelReadColumnName("邮箱")]
        public string Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [ExcelColumnName("手机号", ColumnWith = 30, Sort = 3)]
        [ExcelReadColumnName("手机号")]
        public string Mobile { get; set; }
        /// <summary>
        /// QQ号
        /// </summary>
        [ExcelColumnName("QQ号", ColumnWith = 30, Sort = 4)]
        [ExcelReadColumnName("QQ号")]
        public string QQ { get; set; }
        /// <summary>
        /// 工号=登陆账号
        /// </summary>
        [ExcelColumnName("登陆账号", ColumnWith = 30, Sort = 5)]
        [ExcelReadColumnName("登陆账号")]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        [ExcelColumnName("微信号", ColumnWith = 30, Sort = 6)]
        public string WeChat { get; set; }
        /// <summary>
        /// 员工类型
        /// </summary>
        public int EmpliyeeType { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime? EntryDate { get; set; }
        /// <summary>
        /// 职务名称
        /// </summary>
        [ExcelColumnName("职务名称", ColumnWith = 30, Sort = 7)]
        public string JobName { get; set; }
        /// <summary>
        /// 账号状态
        /// </summary>
        public StateEnum AccountState { get; set; } = StateEnum.Normal;
        /// <summary>
        /// 组织Id{用户所属组织 Or 公司}
        /// </summary>
        public Guid OrganizeId { get; set; }
        /// <summary>
        /// 部门Id{用户所属部门}
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// 过滤字段
        /// </summary>
        [NotMapped]
        public string RoleIds { get; set; }
    }
}
