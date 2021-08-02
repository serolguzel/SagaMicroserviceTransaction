using System;
using SagaMicroserviceTransaction.Domain.Model;
using SagaMicroserviceTransaction.Domain.Model.Structs;

namespace SagaMicroserviceTransaction.Domain
{
    public interface ISagaStateMachine
    {
        SagaId Id { get; }
        Type Type { get; }
        SagaStates State { get; }
        object Data { get; }
        void Update(SagaStates state, object data = null);
    }
}
