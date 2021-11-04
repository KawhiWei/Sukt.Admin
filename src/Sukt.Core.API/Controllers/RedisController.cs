using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sukt.Core.Dtos.ShoopingCart;
using Sukt.Core.Shared;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.OperationResult;
using Sukt.Module.Core.ResultMessageConst;
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
                    return new OperationResponse("成功获取到锁", Module.Core.Enums.OperationEnumType.Success).ToAjaxResult();
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
        /// <summary>
        /// 测试redis写购物车
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [Description("测试redis写购物车")]
        public async Task<AjaxResult> CreateShoopingCartAsync(Guid id, [FromBody] Product request)
        {
            var carlist = new List<Product>();
            var exist = await _redisRepository.ExistAsync(id.ToString());
            if (exist)
            {
                var str = await _redisRepository.GetStringAsync(id.ToString());
                carlist = JsonConvert.DeserializeObject<List<Product>>(str);
                carlist.Add(request);
                await _redisRepository.SetJsonAsync(id.ToString(), carlist, TimeSpan.FromDays(360));
            }
            else
            {
                carlist.Add(request);
                await _redisRepository.SetJsonAsync(id.ToString(), carlist, TimeSpan.FromDays(360));
            }
            return new OperationResponse(ResultMessage.InsertSuccess, Module.Core.Enums.OperationEnumType.Success).ToAjaxResult();
        }
        /// <summary>
        /// 测试读取redis购物车商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Description("测试读取redis购物车商品")]
        public async Task<AjaxResult> GetShoopingCartAsync(Guid id)
        {
            var carlist = new List<Product>();
            var exist = await _redisRepository.ExistAsync(id.ToString());
            if (exist)
            {
                var str = await _redisRepository.GetStringAsync(id.ToString());
                carlist = JsonConvert.DeserializeObject<List<Product>>(str);
            }
            return new OperationResponse(ResultMessage.InsertSuccess, carlist, Module.Core.Enums.OperationEnumType.Success).ToAjaxResult();
        }
    }
}
