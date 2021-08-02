using System.Threading.Tasks;
using SagaMicroserviceTransaction.Domain;
using SagaMicroserviceTransaction.Domain.Managers;
using SagaMicroserviceTransaction.Domain.Model;
using SagaMicroserviceTransaction.Persistence;
using SagaMicroserviceTransaction.Persistence.Abstractions;

namespace SagaMicroserviceTransaction.Managers
{
    public class Processor : IProcessor
    {
        private readonly ISagaStateRepository repository;
        private readonly ISagaLog log;

        public Processor(ISagaStateRepository repository, ISagaLog log)
        {
            this.repository = repository;
            this.log = log;
        }

        public async Task ProcessAsync<TData, TMessage>(IMicroserviceSaga saga, TMessage message, ISagaStateMachine state, IMicroserviceSagaContext context) where TMessage : class
        {
            var action = (ISagaMethod<TMessage>)saga;

            try
            {
                await action.HandleAsync(message, context);
            }
            catch (SagaException ex)
            {
                context.SagaContextError = new SagaContextError(ex);

                if (!(saga.State is SagaStates.Rejected))
                {
                    saga.Reject(ex);
                }
            }
            finally
            {
                await UpdateSagaAsync<TData, TMessage>(message, saga, state);
            }
        }

        private async Task UpdateSagaAsync<TData, TMessage>(TMessage message, IMicroserviceSaga saga, ISagaStateMachine state)
            where TMessage : class
        {
            var sagaType = saga.GetType();
            var updatedSagaData = sagaType.GetProperty(nameof(IMicroserviceSaga<object>.Data))?.GetValue(saga);

            state.Update(saga.State, updatedSagaData);
            var logData = SagaLoggerBase.Create(saga.Id, sagaType, message);

            var persistenceTasks = new []
            {
                repository.WriteAsync(state),
                log.WriteAsync(logData)
            };

            await Task.WhenAll(persistenceTasks).ConfigureAwait(false);
        }
        
    }
}
