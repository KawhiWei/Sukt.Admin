using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sukt.Core.Application.Test;
using Sukt.Core.Dtos.Menu;
using Sukt.Core.Shared;
using Sukt.Module.Core.OperationResult;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Sukt.Core.API.Controllers
{
    /// <summary>
    /// 压测消息总线
    /// </summary>
    [Description("压测消息总线")]
    public class TestMQController : ApiControllerBase
    {
        private readonly ITestMQ _test;
        public TestMQController(ITestMQ test)
        {
            _test = test;
        }
        /// <summary>
        /// TestMQ Rent
        /// 租用的方式发送消息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("测试提交数据")]
        public async Task<AjaxResult> CreateOrder([FromBody] MenuInputDto dto)
        {
            return (await _test.SenderOrder(dto)).ToAjaxResult();
        }
        /// <summary>
        /// TestMQ No Rent
        /// 不租用的方式发送消息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("测试提交数据")]
        public async Task<AjaxResult> CreateOrderA([FromBody] MenuInputDto dto)
        {
            return (await _test.SenderOrder(dto,1)).ToAjaxResult();
        }
    }
}
