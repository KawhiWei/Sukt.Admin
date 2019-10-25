using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UwlAPI.Tools.AuthHelper.Policys
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtToken
    {
        /// <summary>
        /// 获取基于JWT的Token
        /// </summary>
        /// <param name="claims">需要在登录的时候配置</param>
        /// <param name="permissionRequirement">在startup中定义的参数</param>
        /// <returns></returns>
        public static dynamic BuildJwtToken(Claim[] claims, PermissionRequirement permissionRequirement)
        {
            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                issuer:permissionRequirement.Issuer,//设置发行人
                audience:permissionRequirement.Audience,//设置订阅人
                claims:claims,//设置角色
                notBefore:now,//开始时间
                expires:now.Add(permissionRequirement.Expiration),//设置过期时间
                signingCredentials:permissionRequirement.SigningCredentials//设置签名认证
                );
            var enCodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            //打包返回前台
            var responseJson = new
            {
                success = true,
                token = enCodeJwt,
                expires_in = permissionRequirement.Expiration.TotalSeconds,
                token_type = "Bearer"
            };
            return responseJson;
        }
    }
}
