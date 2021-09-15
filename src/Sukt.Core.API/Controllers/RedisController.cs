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
            //Console.WriteLine($"--------控制器当前线程ID{ Thread.CurrentThread.ManagedThreadId}");
            var result = new OperationResponse();
            var lookkey = "miaoshakoujiankucun";
            try
            {
                result.Success = await _redisRepository.LockAsync(lookkey, TimeSpan.FromSeconds(5));
                if (result.Success)
                {
                    result.Message = "成功获取到锁";
                    Console.WriteLine("成功获取到锁");
                    await Task.Delay(4000);
                    await _redisRepository.UnLockAsync(lookkey);
                }
                else
                {
                    result.Message = "获取锁失败";
                    Console.WriteLine("获取锁失败");
                }
                return result.ToAjaxResult();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await _redisRepository.UnLockAsync(lookkey);
                
            }
            return result.ToAjaxResult();
        }
    }
}
