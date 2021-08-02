using Microsoft.Extensions.DependencyInjection;
using SagaMicroserviceTransaction.Persistence.Abstractions;

namespace SagaMicroserviceTransaction.DependencyInjection
{
    public interface IMicroserviceSagaBuilder
    {
        IServiceCollection Services { get; }
        IMicroserviceSagaBuilder UseInMemoryPersistence();
        IMicroserviceSagaBuilder UseSagaLog<TSagaLog>() where TSagaLog : ISagaLog;
        IMicroserviceSagaBuilder UseSagaStateRepository<TRepository>() where TRepository : ISagaStateRepository;
    }
}
