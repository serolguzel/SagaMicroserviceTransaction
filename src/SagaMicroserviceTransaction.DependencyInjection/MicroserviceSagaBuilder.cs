using Microsoft.Extensions.DependencyInjection;
using SagaMicroserviceTransaction.Persistence;
using SagaMicroserviceTransaction.Persistence.Abstractions;

namespace SagaMicroserviceTransaction.DependencyInjection
{
    public class MicroserviceSagaBuilder : IMicroserviceSagaBuilder
    {
        public IServiceCollection Services { get; }

        public MicroserviceSagaBuilder(IServiceCollection services)
            => Services = services;

        
        public IMicroserviceSagaBuilder UseInMemoryPersistence()
        {
            Services.AddSingleton(typeof(ISagaStateRepository), typeof(InMemorySagaStateRepository));
            Services.AddSingleton(typeof(ISagaLog), typeof(InMemorySagaLog));
            return this;
        }

        public IMicroserviceSagaBuilder UseSagaLog<TSagaLog>() where TSagaLog : ISagaLog
        {
            Services.AddTransient(typeof(ISagaLog), typeof(TSagaLog));
            return this;
        }

        public IMicroserviceSagaBuilder UseSagaStateRepository<TRepository>() where TRepository : ISagaStateRepository
        {
            Services.AddTransient(typeof(ISagaStateRepository), typeof(TRepository));
            return this;
        }
    }
}