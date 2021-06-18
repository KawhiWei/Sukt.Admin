using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sukt.Core.Shared;
using Sukt.Core.Dtos.Identity.Role;
using Sukt.Module.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.Core.API.Controllers
{
    /// <summary>
    /// 文件上传
    /// </summary>
    [Description("文件上传")]
    [AllowAnonymous]
    public class UpLoadController : ApiControllerBase
    {
        /// <summary>
        /// 文件分片上传接口
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableRequestSizeLimit]
        [Description("文件分片上传")]
        public async Task<string> Upload([FromForm] IFormCollection input)
        {

            await Task.CompletedTask;
            return "";
        }
    }
}
