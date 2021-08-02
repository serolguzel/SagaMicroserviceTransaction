using System.Collections.Generic;

namespace SagaMicroserviceTransaction.Domain.Managers
{
    public interface ISearcher
    {
        IEnumerable<ISagaMethod<TMessage>> Search<TMessage>();
    }
}
