using SagaMicroserviceTransaction.Domain;

namespace SagaMicroserviceTransaction.Persistence
{
    public sealed class ContextMetadata : IContextMetadata
    {
        public string Key { get; }
        public object Value { get; }

        public ContextMetadata(string key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}
