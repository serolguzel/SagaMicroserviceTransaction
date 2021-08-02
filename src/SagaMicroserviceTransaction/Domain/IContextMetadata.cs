namespace SagaMicroserviceTransaction.Domain
{
    public interface IContextMetadata
    {
        string Key { get; }
        object Value { get; }
    }
}
