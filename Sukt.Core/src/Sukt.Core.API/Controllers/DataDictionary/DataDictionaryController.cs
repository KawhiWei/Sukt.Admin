using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sukt.Core.Application.Contracts.DictionaryContract;
using Sukt.Core.AspNetCore.ApiBase;
using Sukt.Core.Dtos.DataDictionaryDto;

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
    }
}