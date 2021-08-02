using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SagaMicroserviceTransaction.Domain;
using SagaMicroserviceTransaction.Domain.Model.Structs;
using SagaMicroserviceTransaction.Persistence.Abstractions;

namespace SagaMicroserviceTransaction.Persistence
{
    public class InMemorySagaLog : ISagaLog
    {
        private readonly List<ILogerBaseEntity> sagaLog;

        public InMemorySagaLog()
        {
            sagaLog = new List<ILogerBaseEntity>();
        }

        public Task<IEnumerable<ILogerBaseEntity>> ReadAsync(SagaId id, Type type)
        {
            return Task.FromResult(sagaLog.Where(sld => sld.Id == id && sld.Type == type));
        }

        public async Task WriteAsync(ILogerBaseEntity message)
        {
            sagaLog.Add(message);
            await Task.CompletedTask;
        }
    }
}