using System;

namespace SagaMicroserviceTransaction.Domain.Model.Structs
{
    public struct SagaId
    {
        private string Id { get; }

        private SagaId(string id)
            => Id = id;

        public static implicit operator string(SagaId sagaId) => sagaId.Id;

        public static implicit operator SagaId(string sagaId)
            => new SagaId(sagaId);

        public static SagaId NewSagaId()
            => new SagaId(Guid.NewGuid().ToString());

        public override string ToString() => Id;
    }
}
