using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SagaMicroserviceTransaction.Domain;
using SagaMicroserviceTransaction.Domain.Model.Structs;

namespace SagaMicroserviceTransaction.Persistence.Abstractions
{
    public interface ISagaLog
    {
        Task<IEnumerable<ILogerBaseEntity>> ReadAsync(SagaId id, Type type);
        Task WriteAsync(ILogerBaseEntity message);
    }
}