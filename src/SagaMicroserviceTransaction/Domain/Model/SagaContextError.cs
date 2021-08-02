namespace SagaMicroserviceTransaction.Domain.Model
{
    public class SagaContextError
    {
        public SagaException Exception { get; }

        public SagaContextError(SagaException e)
        {
            Exception = e;
        }
    }
}
