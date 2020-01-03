using UwlAPI.Tools.AuthHelper.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Uwl.Common.Helper;
using Uwl.Extends.Utility;

namespace UwlAPI.Tools.AuthHelper.Token
{
    /// <summary>
    /// 自定义中间件生成Token
    /// </summary>
    public class JwtHelper
    {
        static IConfiguration _configuration { get; }
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string IssueJWT(TokenModelJWT tokenModel)
        {
            var Issuer = Appsettings.app(new string[] { "JwtSettings", "Issuer" });
            var Audience = Appsettings.app(new string[] { "JwtSettings", "Audience" });
            var SecretKey = Appsettings.app(new string[] { "JwtSettings", "SecretKey" });
            var dateTime = DateTime.UtcNow;
            var jwtSettings = new JwtSettings()
            {
                Issuer = Issuer,//Appsettings.app(new string[] { "JwtSettings", "Issuer" }),
                Audience= Audience,//Appsettings.app(new string[] { "JwtSettings", "Audience" }),
                SecretKey = SecretKey,//Appsettings.app(new string[] { "JwtSettings", "SecretKey" }),
            };
            //_configuration.Bind("JwtSettings", jwtSettings);//获取配置
            //配置默认的Claim
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,tokenModel.Uid.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                //这个就是过期时间，目前是过期50秒，可自定义，注意JWT有自己的缓冲过期时间
                new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddDays(7)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss,jwtSettings.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud,jwtSettings.Audience),
                //new Claim(ClaimTypes.Role,tokenModel.Role),//为了解决一个用户多个角色(比如：Admin,System)，用下边的方法
            };
            // 可以将一个用户的多个角色全部赋予；
            // 作者：DX 提供技术支持；
            claims.AddRange(tokenModel.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

            //将未加密的Token进行加密
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            //将未加密的Token进行加密
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);//生成令牌
            //创建JwtSecurityToken，
            var jwt = new JwtSecurityToken
                (
                issuer:jwtSettings.Issuer,
                claims: claims,
                signingCredentials: creds
                );
            var jwthandler = new JwtSecurityTokenHandler();
            //通过JwtSecurityTokenHandler.WriteToken生成Token
            var encodedjwt = jwthandler.WriteToken(jwt);
            return encodedjwt;
        }
        /// <summary>
        /// 反序列化jwt
        /// </summary>
        /// <param name="jwtstr"></param>
        /// <returns></returns>
        public static TokenModelJWT SerializeJwt(string jwtstr)
        {
            var jwthandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwthandler.ReadJwtToken(jwtstr);
            object role = new object();
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
            var tm = new TokenModelJWT
            {
                Uid=jwtToken.Id.ToGuid(),
                Role = role != null ? role.ToString() : "",
            };
            return tm;
        }
    }
    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenModelJWT
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Uid { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 职能
        /// </summary>
        public string Work { get; set; }

    }
}
