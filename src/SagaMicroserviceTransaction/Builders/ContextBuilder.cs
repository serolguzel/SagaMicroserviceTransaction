using System.Collections.Generic;
using SagaMicroserviceTransaction.Domain;
using SagaMicroserviceTransaction.Domain.Model.Structs;
using SagaMicroserviceTransaction.Persistence;

namespace SagaMicroserviceTransaction.Builders
{
    internal sealed class ContextBuilder : IContextBuilder
    {
        private SagaId sagaId;
        private string originator;
        private readonly List<IContextMetadata> metadata;

        public ContextBuilder()
            => metadata = new List<IContextMetadata>();

        public IContextBuilder WithSagaId(SagaId sagaId)
        {
            this.sagaId = sagaId;
            return this;
        }

        public IContextBuilder WithOriginator(string originator)
        {
            this.originator = originator;
            return this;
        }

        public IContextBuilder WithMetadata(string key, object value)
        {
            var metadata = new ContextMetadata(key, value);
            this.metadata.Add(metadata);
            return this;
        }

        public IContextBuilder WithMetadata(IContextMetadata sagaContextMetadata)
        {
            metadata.Add(sagaContextMetadata);
            return this;
        }

        public IMicroserviceSagaContext Build()
            => MicroserviceSagaContext.Create(sagaId, originator, metadata);
    }
}
