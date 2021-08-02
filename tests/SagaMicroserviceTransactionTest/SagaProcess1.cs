using System;
using System.Threading;
using System.Threading.Tasks;
using SagaMicroserviceTransaction;
using SagaMicroserviceTransaction.Domain;

namespace SagaMicroserviceTransactionTest
{
    public class RequestMessage
    {
        
    }
    public class SagaProcess1 : MicroserviceSaga, ISagaBeginMethod<RequestMessage>
    {
        public Task HandleAsync(RequestMessage message, IMicroserviceSagaContext context,
            CancellationToken cancellationToken = default)
        {
            Console.WriteLine("HandleAsync");
            return Task.CompletedTask;
        }

        public Task CompensateAsync(RequestMessage message, IMicroserviceSagaContext context,
            CancellationToken cancellationToken = default)
        {
            Console.WriteLine("CompensateAsync");
            return Task.CompletedTask;
        }
    }
    
    public class SagaProcess2 : MicroserviceSaga, ISagaBeginMethod<RequestMessage>
    {
        public Task HandleAsync(RequestMessage message, IMicroserviceSagaContext context,
            CancellationToken cancellationToken = default)
        {
            Console.WriteLine("HandleAsync");
            return Task.CompletedTask;
        }

        public Task CompensateAsync(RequestMessage message, IMicroserviceSagaContext context,
            CancellationToken cancellationToken = default)
        {
            Console.WriteLine("CompensateAsync");
            return Task.CompletedTask;
        }
    }
    
    
}