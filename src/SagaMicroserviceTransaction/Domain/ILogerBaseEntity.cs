using System;
using SagaMicroserviceTransaction.Domain.Model.Structs;

namespace SagaMicroserviceTransaction.Domain
{
    public interface ILogerBaseEntity
    {
        SagaId Id { get; }
        Type Type { get; }
        long CreatedAt { get; }
        object Message { get; }
    }
}
