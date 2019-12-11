using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UwlAPI.Tools.Models.LoginViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "登录账号不可为空")]
        public string User { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "密码不可为空")]
        public string Password { get; set; }
    }
}
