using System.Collections.Generic;
using System.Linq;
using SagaMicroserviceTransaction.Builders;
using SagaMicroserviceTransaction.Domain;
using SagaMicroserviceTransaction.Domain.Model;
using SagaMicroserviceTransaction.Domain.Model.Structs;

namespace SagaMicroserviceTransaction
{
    public sealed class MicroserviceSagaContext : IMicroserviceSagaContext
    {
        public SagaId SagaId { get; }
        public string Originator { get; }
        public IReadOnlyCollection<IContextMetadata> Metadata { get; }
        public SagaContextError SagaContextError { get; set; }

        private MicroserviceSagaContext(SagaId sagaId, string originator, IEnumerable<IContextMetadata> metadata)
        {
            SagaId = sagaId;
            Originator = originator;

            var contextMetadatas = metadata as IContextMetadata[] ?? metadata.ToArray();
            var areMetadataKeysUnique = contextMetadatas.GroupBy(m => m.Key).All(g => g.Count() is 1);

            if (!areMetadataKeysUnique)
                throw new SagaException("SagaContext error");
            
            Metadata = contextMetadatas.ToList().AsReadOnly();
        }

        public static IMicroserviceSagaContext Empty =>
            new MicroserviceSagaContext(SagaId.NewSagaId(), string.Empty, Enumerable.Empty<IContextMetadata>());


        public static IMicroserviceSagaContext Create(SagaId sagaId, string originator, IEnumerable<IContextMetadata> metadata)
            => new MicroserviceSagaContext(sagaId, originator, metadata);

        public static IContextBuilder Create()  => new ContextBuilder();

        public IContextMetadata GetMetadata(string key) 
            => Metadata.Single(m => m.Key == key);

        public bool TryGetMetadata(string key, out IContextMetadata metadata)
        {
            metadata = Metadata.SingleOrDefault(m => m.Key == key);
            return metadata != null;
        }
    }
}
