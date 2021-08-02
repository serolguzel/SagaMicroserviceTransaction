using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SagaMicroserviceTransaction.Persistence;
using SagaMicroserviceTransaction.Utils;
using SagaMicroserviceTransaction.Domain;
using SagaMicroserviceTransaction.Domain.Managers;
using SagaMicroserviceTransaction.Domain.Model;
using SagaMicroserviceTransaction.Persistence.Abstractions;

namespace SagaMicroserviceTransaction.Managers
{
    public class PostProcessor : IPostProcessor
    {
        private readonly ISagaLog log;

        public PostProcessor(ISagaLog log)
        {
            this.log = log;
        }
        
        public async Task ProcessAsync<TData, TMessage>(IMicroserviceSaga saga, TMessage message, IMicroserviceSagaContext context, 
            Func<TMessage, IMicroserviceSagaContext, Task> onCompleted, Func<TMessage, IMicroserviceSagaContext, Task> onRejected)
        {
            var sagaType = saga.GetType();

            switch (saga.State)
            {
                case SagaStates.Rejected:
                    await onRejected(message, context);
                    await CompensateAsync(saga, sagaType, context);
                    break;
                case SagaStates.Completed:
                    await onCompleted(message, context);
                    break;
            }
        }
        
        private async Task CompensateAsync(IMicroserviceSaga saga, Type sagaType, IMicroserviceSagaContext context)
        {
            var sagaLogs = await log.ReadAsync(saga.Id, sagaType);
            var listObjects = sagaLogs.OrderByDescending(entity => entity.CreatedAt)
                .Select(logger => logger.Message)
                .ToList();
            foreach (var message in listObjects)
            {
                const string compensate = nameof(ISagaMethod<object>.CompensateAsync);
                await (Task)saga.InvokeGeneric(compensate, message, context, CancellationToken.None);
            }
        }
    }
}
