using SagaMicroserviceTransaction.Domain.Model.Structs;

namespace SagaMicroserviceTransaction.Domain
{
    public interface IContextBuilder
    {
        IContextBuilder WithSagaId(SagaId sagaId);
        IContextBuilder WithOriginator(string originator);
        IContextBuilder WithMetadata(string key, object value);
        IContextBuilder WithMetadata(IContextMetadata sagaContextMetadata);
        IMicroserviceSagaContext Build();
    }
}
