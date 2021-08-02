using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SagaMicroserviceTransaction.Domain;
using SagaMicroserviceTransaction.Domain.Model.Structs;

namespace SagaMicroserviceTransaction.Persistence.Abstractions
{
    public interface ISagaStateRepository
    {
        Task<ISagaStateMachine> ReadAsync(SagaId id, Type type);
        Task<List<ISagaStateMachine>> ReadAllAsync();
        Task WriteAsync(ISagaStateMachine state);
    }
}