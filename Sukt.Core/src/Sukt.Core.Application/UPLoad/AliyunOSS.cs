using Aliyun.OSS;
using Microsoft.Extensions.Options;
using Sukt.Core.AliyunOSS;
using Sukt.Core.Shared.OperationResult;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Application.UPLoad
{
    public class AliyunOSSULoad: IAliyunOSS
    {
        private readonly IOptions<OSSOption> _options;
        public AliyunOSSULoad(IOptions<OSSOption> options)
        {
            _options = options;
        }
        // 由用户指定的OSS访问地址、阿里云颁发的AccessKeyId/AccessKeySecret构造一个新的OssClient实例
        public OperationResponse UpLoadFile(string filePathName,string ossFileName)
        {
            using var filestream = new FileStream(filePathName, FileMode.Open);
            var result= GetOssClient().PutObject("", ossFileName, filestream);
            return new OperationResponse();
        }
        private OssClient GetOssClient()
        {
            return new OssClient(_options.Value.AliyunOption.Endpoint, _options.Value.AliyunOption.AccessKeyId, _options.Value.AliyunOption.KccessKeySecret);
        }
    }
}
