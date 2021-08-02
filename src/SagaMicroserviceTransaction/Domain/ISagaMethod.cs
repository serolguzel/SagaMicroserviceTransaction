using System.Threading;
using System.Threading.Tasks;

namespace SagaMicroserviceTransaction.Domain
{
    public interface ISagaMethod<in TMessage>
    {
        Task HandleAsync(TMessage message, IMicroserviceSagaContext context, CancellationToken cancellationToken = default);
        Task CompensateAsync(TMessage message, IMicroserviceSagaContext context, CancellationToken cancellationToken = default);
    }
}
