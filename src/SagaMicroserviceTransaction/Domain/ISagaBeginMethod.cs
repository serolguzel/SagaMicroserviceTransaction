namespace SagaMicroserviceTransaction.Domain
{
    public interface ISagaBeginMethod<in TMessage> : ISagaMethod<TMessage>
    {
    }
}
