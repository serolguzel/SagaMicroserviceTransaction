using System.Collections.Generic;
using SagaMicroserviceTransaction.Domain.Model;
using SagaMicroserviceTransaction.Domain.Model.Structs;

namespace SagaMicroserviceTransaction.Domain
{
    public interface IMicroserviceSagaContext
    {
        SagaId SagaId { get; }
        string Originator { get; }
        IReadOnlyCollection<IContextMetadata> Metadata { get; }
        IContextMetadata GetMetadata(string key);
        bool TryGetMetadata(string key, out IContextMetadata metadata);
        SagaContextError SagaContextError { get; set; }
    }
}
