using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sukt.Core.Shared;
using Sukt.Module.Core.OperationResult;
using Sukt.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.Core.API.Controllers
{
    public class RedisController : ApiControllerBase
    {
        private readonly IRedisRepository _redisRepository;

        public RedisController(IRedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }
        /// <summary>
        /// 测试redis锁
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("测试redis锁")]
        public async Task<AjaxResult> TestSetRedisLookAsync()
        {
            var lookkey = "miaoshakoujiankucun";
            try
            {
                var lockkeyExist = await _redisRepository.LockAsync(lookkey, TimeSpan.FromSeconds(5));
                if (lockkeyExist)
                {
                    Console.WriteLine($"成功获取到锁{DateTime.Now.ToLongTimeString()}");
                    await Task.Delay(4000);
                    await _redisRepository.UnLockAsync(lookkey);
                    return new OperationResponse("成功获取到锁",Module.Core.Enums.OperationEnumType.Success).ToAjaxResult();
                }
                else
                {
                    Console.WriteLine($"获取锁失败{DateTime.Now.ToLongTimeString()}");
                    return new OperationResponse("获取锁失败", Module.Core.Enums.OperationEnumType.Error).ToAjaxResult();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await _redisRepository.UnLockAsync(lookkey);
            }
        }
    }
}
