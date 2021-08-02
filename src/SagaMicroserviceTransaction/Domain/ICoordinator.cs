using System;
using System.Threading.Tasks;

namespace SagaMicroserviceTransaction.Domain
{
    public interface ICoordinator
    {
        Task ProcessAsync<TData, TMessage>(TMessage message, IMicroserviceSagaContext context = null) where TMessage : class;
        Task ProcessAsync<TData, TMessage>(TMessage message, Func<TMessage, IMicroserviceSagaContext, Task> onCompleted = null, Func<TMessage, IMicroserviceSagaContext, Task> onRejected = null, IMicroserviceSagaContext context = null) where TMessage : class;       
    }
}
