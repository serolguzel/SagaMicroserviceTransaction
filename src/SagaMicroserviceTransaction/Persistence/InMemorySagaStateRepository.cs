using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SagaMicroserviceTransaction.Domain;
using SagaMicroserviceTransaction.Domain.Model.Structs;
using SagaMicroserviceTransaction.Persistence.Abstractions;

namespace SagaMicroserviceTransaction.Persistence
{
    public class InMemorySagaStateRepository : ISagaStateRepository
    {
        private readonly List<ISagaStateMachine> repository;

        public InMemorySagaStateRepository() => repository = new List<ISagaStateMachine>();

        public Task<ISagaStateMachine> ReadAsync(SagaId id, Type type) 
            => Task.FromResult(repository.FirstOrDefault(s => s.Id == id && s.Type == type));

        public Task<List<ISagaStateMachine>> ReadAllAsync()
        {
            return Task.FromResult(repository);
        }

        public async Task WriteAsync(ISagaStateMachine state)
        {
            var sagaDataToUpdate = await ReadAsync(state.Id, state.Type);

            repository.Remove(sagaDataToUpdate);
            repository.Add(state);

            await Task.CompletedTask;
        }
    }
}