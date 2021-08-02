using System.Threading.Tasks;

namespace SagaMicroserviceTransaction.Domain.Managers
{
    public interface IProcessor
    {
        Task ProcessAsync<TData, TMessage>(IMicroserviceSaga saga, TMessage message, ISagaStateMachine state, IMicroserviceSagaContext context) where TMessage : class;
    }
}