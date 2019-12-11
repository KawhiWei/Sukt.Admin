using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UwlAPI.Tools.AuthHelper.JWT;
using UwlAPI.Tools.AuthHelper.Token;
using UwlAPI.Tools.Models.LoginViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Uwl.Common;
using Uwl.Common.Utility;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Server.UserServices;
using Uwl.Data.Model.Result;
using UwlAPI.Tools.AuthHelper.Policys;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Uwl.Common.Download;
using SignalRDemo.SignalrHubs;
using Uwl.Extends.EncryPtion;
using Uwl.Common.RabbitMQ;
using Uwl.QuartzNet.JobCenter.Center;
using Uwl.Common.Cache.RedisCache;
using Microsoft.AspNetCore.Cors;
using Uwl.Extends.Utility;
using Microsoft.Extensions.Logging;

namespace UwlAPI.Tools.Controllers
{
    /// <summary>
    /// 登录或者获取Token接口不加//[Authorize(Policy = "Admin")]权限，加了类似，你把钥匙上了锁
    /// </summary>
    //次特性是必须带jwtToken才可以请求，如果在获取Token的控制器上加了此特性需要在获取Token的方法上添加[AllowAnonymous]//对获取token得方法加允许匿名标注
    [AllowAnonymous]//对获取token得方法加允许匿名标注//不受授权控制，任何人都可访问
    //[Produces("application/json")]
    //[EnableCors("AllRequests")]
    [Route("api/Login")]
    public class AuthorizeController : BaseController<AuthorizeController>
    {
        private JwtSettings _jwtSettings;
        private IUserServer _userserver;
        private IRedisCacheManager _redisCacheManager;
        private readonly IRabbitMQ _rabbitMQ;
        private readonly PermissionRequirement _requirement;
        private readonly IHostingEnvironment _hostingEnvironment;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_jwtSettingsAccesser"></param>
        /// <param name="userServer"></param>
        /// <param name="redisCacheManager"></param>
        /// <param name="rabbitMQ">消息队列</param>
        /// <param name="permissionRequirement"></param>
        /// <param name="hostingEnvironment"></param>
        /// <param name="schedulerCenter"></param>
        ///  <param name="logger"></param>
        public AuthorizeController(IOptions<JwtSettings> _jwtSettingsAccesser,
            IUserServer userServer, IRedisCacheManager redisCacheManager, IRabbitMQ rabbitMQ,
            PermissionRequirement permissionRequirement, IHostingEnvironment hostingEnvironment, ISchedulerCenter schedulerCenter, ILogger<AuthorizeController> logger
            ) :base(logger)
        {
            this._jwtSettings = _jwtSettingsAccesser.Value;
            this._userserver = userServer;
            this._redisCacheManager = redisCacheManager;
            this._requirement = permissionRequirement;
            this._hostingEnvironment = hostingEnvironment;
            this._rabbitMQ = rabbitMQ;
        }
        #region 获取Token No.1
        /// <summary>
        /// 官方的方式获取Token
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Token")]
        [AllowAnonymous]//对获取token得方法加允许匿名标注//不受授权控制，任何人都可访问
        public async Task<string> Token([FromBody] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                SysUser user = await _userserver.CheckUser(loginViewModel.User, loginViewModel.Password);
                //判断用户名密码是否正确，如果不正确返回Token  !(loginViewModel.User=="avery"&& loginViewModel.Password=="123")
                if (user == null)
                {
                    return "账号或者密码错误";
                }
                else
                {
                    #region MyRegion
                    var Ip = HttpContext.GetClientIP();
                    //var claim = new Claim[]
                    //{
                    //    new Claim(ClaimTypes.Name,user.Account),
                    //    new Claim(ClaimTypes.Role,user.Account),
                    //};
                    ////设置对称秘钥
                    //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                    ////生成签名证书(秘钥，加密算法)
                    //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    ////生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，并引用System.IdentityModel.Tokens.Jwt命名空间
                    //var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claim, DateTime.Now, DateTime.Now.AddDays(1), creds);
                    #endregion
                    TokenModelJWT tokenModel = new TokenModelJWT()
                    {
                        Uid = user.Id,
                        Role = "Admin",
                    };
                    var token = JwtHelper.IssueJWT(tokenModel);
                    try
                    {
                        //var ss= DateTime.Now;
                        //_redisCacheManager.Set("Id", new { Id = 12 },ss.TimeOfDay);
                        //_log.Add("创建TOken", "用户登陆", Ip,EnumTypes.其他分类);
                        return token;
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
            }
            return "账号或者密码错误";
        }
        #endregion


        #region 自定义中间件获取Token No.2
        /// <summary>
        /// 第二种方式获取Token
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CustomGetToken")]
        public IActionResult GetJWTStr([FromBody] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                SysUser user = new SysUser(); //await _userserver.CheckUser(loginViewModel.User, loginViewModel.Password);
                //判断用户名密码是否正确，如果不正确返回Token  !(loginViewModel.User=="avery"&& loginViewModel.Password=="123")
                if (user == null)
                {
                    return Json(new OperationResult(ResultType.Error, "账号或者密码错误"));
                }
                else
                {
                    var Ip = HttpContext.GetClientIP();
                    TokenModelJWT tokenModel = new TokenModelJWT()
                    {
                        Uid = user.Id,
                        Role = "Admin",
                    };
                    try
                    {
                        string jwtstr = JwtHelper.IssueJWT(tokenModel);
                        return Ok(new { token = jwtstr });
                    }
                    catch (Exception ex)
                    {

                        return Json("" + ex.Message);
                    }
                }
            }
            return BadRequest();
        }
        #endregion

        #region 获取Token No.3
        /// <summary>
        /// 自定义策略授权JWT，控制到Action级别权限
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("TokenThree")]
        public async Task<MessageModel<dynamic>> TokenAssig([FromBody] LoginViewModel loginViewModel)
        {
            var data = new MessageModel<dynamic>();
            var cheke = loginViewModel.CheckModel();
            if (cheke.Item1)
            {
                loginViewModel.Password = loginViewModel.Password.ToMD5();
                var Ip = HttpContext.GetClientIP();
                //await Console.Out.WriteAsync($"IP为【{Ip}】的客户机访问");
                SysUser Info = await _userserver.CheckUser(loginViewModel.User, loginViewModel.Password);
                #region    QuartzNet定时任务
                //await _schedulerCenter.AddScheduleJobAsync(new SysSchedule
                //{
                //    Name = "test1",
                //    JobGroup = "test1group",
                //    AssemblyName = "Uwl.QuartzNet.JobCenter",
                //    ClassName = "Simple",
                //    RunTimes = 0,
                //    IntervalSecond =4,
                //});
                //await _schedulerCenter.AddScheduleJobAsync(new SysSchedule
                //{
                //    Name = "testSimpleTwo",
                //    JobGroup = "test1group",
                //    AssemblyName = "Uwl.QuartzNet.JobCenter",
                //    ClassName = "Simple",
                //    RunTimes = 0,
                //    IntervalSecond = 9,
                //});
                //await _schedulerCenter.AddScheduleJobAsync(new SysSchedule
                //{
                //    Name = "testSimpleThree",
                //    JobGroup = "test1group",
                //    AssemblyName = "Uwl.QuartzNet.JobCenter",
                //    ClassName = "Simple",
                //    RunTimes = 0,
                //    IntervalSecond = 5,
                //});
                //_rabbitMQ.SendData("hello", Info);
                #endregion
                if (Info == null)
                {
                    data.msg = "账号或者密码错误";
                    return data;
                }
                else
                {
                    try
                    {
                        //_schedulerCenter.AddScheduleJobAsync<SysSchedule>(new SysSchedule());
                        var RoleName = await _userserver.GetUserRoleByUserId(Info.Id);
                        var claims = new List<Claim>
                        {
                        new Claim(ClaimTypes.Name,Info.Name),//设置用户名称
                        new Claim(JwtRegisteredClaimNames.Jti,Info.Id.ToString()),//设置用户ID
                        new Claim(ClaimTypes.Expiration,DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString()),//设置过期时间
                        new Claim("Id",Info.Id.ToString()),
                        new Claim("userName",Info.Name)
                        };
                        claims.AddRange(RoleName.Split(',').Select(x => new Claim(ClaimTypes.Role, x)));//将用户角色填充到claims中
                        var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);//用户标识
                        identity.AddClaims(claims);
                        var token = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
                        data.response = token;
                        data.msg = "Token获取成功";
                        data.success = true;
                        return data;
                    }
                    catch (Exception ex)
                    {
                        data.msg = "获取角色信息失败" + ex.Message;
                        return data;
                    }
                }
            }
            else
            {
                data.msg = cheke.Item2;
                return data;
            }
        }
        #endregion
    }
}