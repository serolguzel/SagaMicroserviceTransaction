namespace SagaMicroserviceTransaction.Domain.Model
{
    public enum SagaStates : byte
    {
        Pending = 0,
        Completed = 1,
        Rejected = 2,
    }
}