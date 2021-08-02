using System;
using SagaMicroserviceTransaction.Domain;
using SagaMicroserviceTransaction.Domain.Model.Structs;

namespace SagaMicroserviceTransaction.Persistence
{
    public class SagaLoggerBase : ILogerBaseEntity
    {
        public SagaId Id { get; }
        public Type Type { get; }
        public long CreatedAt { get; }
        public object Message { get; }

        private SagaLoggerBase(SagaId sagaId, Type sagaType, long createdAt, object message) 
            => (Id, Type, CreatedAt, Message) = (sagaId, sagaType, createdAt, message);

        public static ILogerBaseEntity Create(SagaId sagaId, Type sagaType, object message) 
            => new SagaLoggerBase(sagaId, sagaType, DateTimeOffset.Now.GetTimeStamp(), message);
    }
}
