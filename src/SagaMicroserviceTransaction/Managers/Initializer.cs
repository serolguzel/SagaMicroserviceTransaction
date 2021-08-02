using System;
using System.Threading.Tasks;
using SagaMicroserviceTransaction.Utils;
using SagaMicroserviceTransaction.Domain;
using SagaMicroserviceTransaction.Domain.Managers;
using SagaMicroserviceTransaction.Domain.Model;
using SagaMicroserviceTransaction.Domain.Model.Structs;
using SagaMicroserviceTransaction.Persistence;
using SagaMicroserviceTransaction.Persistence.Abstractions;

namespace SagaMicroserviceTransaction.Managers
{
    public class Initializer : IInitializer
    {
        private readonly ISagaStateRepository repository;
        public Initializer(ISagaStateRepository repository)
        {
            this.repository = repository;
        }
        
        public async Task<(bool isInitialized, ISagaStateMachine state)> TryInitializeAsync<TData, TMessage>(IMicroserviceSaga saga, SagaId id, TMessage _)
        {
            var action = (ISagaMethod<TMessage>)saga;
            var sagaType = saga.GetType();
            var dataType = saga.GetSagaDataType();
            
            // var state = await repository.ReadAsync<TData>(id, sagaType).ConfigureAwait(false);
            var state = await repository.ReadAsync(id, sagaType).ConfigureAwait(false);
            if (state is null)
            {
                if (!(action is ISagaBeginMethod<TMessage>))
                {
                    return (false, default);
                }

                state = CreateSagaState(id, sagaType, dataType);
            }
            else if (state.State is SagaStates.Rejected)
            {
                return (false, default);;
            }

            InitializeSaga(saga, id, state);

            return (true, state);
        }
        
        private static ISagaStateMachine CreateSagaState(SagaId id, Type sagaType, Type dataType)
        {
            var sagaData = dataType != null ? Activator.CreateInstance(dataType) : null;
            return SagaStateMachine.Create(id, sagaType, SagaStates.Pending, sagaData);
        }

        private static void InitializeSaga(IMicroserviceSaga saga, SagaId id, ISagaStateMachine state)
        {
            if (state.Data is null)
            {
                saga.Initialize(id, state.State);
            }
            else
            {
                saga.InvokeGeneric(nameof(IMicroserviceSaga<object>.Initialize), id, state.State, state.Data);
            }
        }
    }
}
