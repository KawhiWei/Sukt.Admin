using System.ComponentModel.DataAnnotations;

namespace Sukt.Core.Dtos.LoginIdentity
{
    public class LoginInputDto
    {
        /// <summary>
        /// 用户名登录名
        /// </summary>

        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}