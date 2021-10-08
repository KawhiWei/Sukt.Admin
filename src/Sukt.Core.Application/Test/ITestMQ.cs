using Sukt.Core.Dtos.Menu;
using Sukt.Module.Core;
using Sukt.Module.Core.OperationResult;
using Sukt.MQTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Application.Test
{
    public interface ITestMQ: ISuktMQTransactionSubscribe, IScopedDependency
    {
        Task<OperationResponse> SenderOrder(MenuInputDto dto, int isrent = 2);
        void GetOrder(MenuInputDto dto);
    }
    public class TestMQ : ITestMQ
    {
        private readonly IMQTransactionPublisher _transactionPublisher;

        public TestMQ(IMQTransactionPublisher transactionPublisher)
        {
            _transactionPublisher = transactionPublisher;
        }

        public async Task<OperationResponse> SenderOrder(MenuInputDto dto, int isrent = 2)
        {
            await _transactionPublisher.PublishAsync("sukt.mqtransaction.order", "mqtransaction.keys.order", dto, 1);
            return new OperationResponse();
        }
        [SuktMQSubscribe(exchange: "sukt.mqtransaction.order", topicOrRoutingKeyName: "mqtransaction.keys.order")]
        public void GetOrder(MenuInputDto dto)
        {
            //throw new Exception("我报错了");
            Console.WriteLine($"消费成功------>{dto.ComponentName}");
        }
    }
}
