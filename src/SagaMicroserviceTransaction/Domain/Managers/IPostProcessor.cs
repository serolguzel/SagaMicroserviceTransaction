using System;
using System.Threading.Tasks;

namespace SagaMicroserviceTransaction.Domain.Managers
{
    public interface IPostProcessor
    {
        Task ProcessAsync<TData, TMessage>(IMicroserviceSaga saga, TMessage message, IMicroserviceSagaContext context,
            Func<TMessage, IMicroserviceSagaContext, Task> onCompleted, Func<TMessage, IMicroserviceSagaContext, Task> onRejected);
    }
}