using System.Threading.Tasks;
using SagaMicroserviceTransaction.Domain.Model.Structs;

namespace SagaMicroserviceTransaction.Domain.Managers
{
    public interface IInitializer
    {
        Task<(bool isInitialized, ISagaStateMachine state)> TryInitializeAsync<TData, TMessage>(IMicroserviceSaga saga, SagaId id, TMessage _);
    }
}
