using Aliyun.OSS;
using Sukt.Core.Dtos.Menu;
using Sukt.Core.Shared.Events.EventBus;
using Sukt.Core.Shared.OperationResult;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Application.Test
{
    public class TestIRequest : ITestIRequest
    {
        private readonly IMediatorHandler _mediatorbus;

        public TestIRequest(IMediatorHandler mediatorbus)
        {
            _mediatorbus = mediatorbus;
        }

        public async Task<OperationResponse> TestIRequset(string str)
        {
            await Task.CompletedTask;

            var result = await _mediatorbus.SendAsync(new TestEnevtRequest() { Test="1as3d13asd13as"});
            return new OperationResponse();
        }
        public async Task TestAliyunOSSDSK()
        {
            const string accessKeyId = "<yourAccessKeyId>";
            const string accessKeySecret = "<yourAccessKeySecret>";
            const string endpoint = "http://oss-cn-hangzhou.aliyuncs.com";
            // 由用户指定的OSS访问地址、阿里云颁发的AccessKeyId/AccessKeySecret构造一个新的OssClient实例。
            var ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret);
        }
    }
}
