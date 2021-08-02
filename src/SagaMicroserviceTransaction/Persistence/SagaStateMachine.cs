using System;
using SagaMicroserviceTransaction.Domain;
using SagaMicroserviceTransaction.Domain.Model;
using SagaMicroserviceTransaction.Domain.Model.Structs;

namespace SagaMicroserviceTransaction.Persistence
{
    public class SagaStateMachine : ISagaStateMachine
    {
        public SagaId Id { get; }
        public Type Type { get; }
        public SagaStates State { get; private set; }
        public object Data { get; private set; }

        public SagaStateMachine(SagaId id, Type type, SagaStates state, object data) 
            => (Id, Type, State, Data) = (id, type, state, data);

        public static ISagaStateMachine Create(SagaId id, Type type, SagaStates state, object data = null) 
            => new SagaStateMachine(id, type, state, data);

        public void Update(SagaStates state, object data = null)
        {
            State = state;
            Data = data;
        }
    }
}
