using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sukt.Core.Application.Contracts;
using Sukt.Core.AspNetCore.ApiBase;
using Sukt.Core.Dtos.DataDictionaryDto;
using Sukt.Core.Shared.AjaxResult;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.Extensions.ResultExtensions;

namespace Sukt.Core.API.Controllers.DataDictionary
{
    [ApiController]
    public class DataDictionaryController : ApiControllerBase
    {
        private readonly IDictionaryContract _dictionary=null;
        private readonly ILogger<DataDictionaryController> _logger=null;

        public DataDictionaryController(IDictionaryContract dictionary, ILogger<DataDictionaryController> logger)
        {
            _dictionary = dictionary;
            _logger = logger;
        }
        /// <summary>
        /// 添加一个数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateAsync(DataDictionaryInputDto input)
        {
            return await _dictionary.InsertAsync(input);
        }
        /// <summary>
        /// 添加一个数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageList<DataDictionaryOutDto>> GetPageAsync([FromQuery]BaseQuery input)
        {
            return (await _dictionary.GetResultAsync(input)).PageList();
        }
        /// <summary>
        /// 获取组织架构
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("获取组织架构")]
        public async Task<TreeData<TreeDictionaryOutDto>> GetTreeAsync()
        {

            var result = await _dictionary.GetTreeAsync();
            return new TreeData<TreeDictionaryOutDto>()
            {
                Data = result.Data,
                Message = result.Message,
                Success = result.Success
            };
        }
    }
}