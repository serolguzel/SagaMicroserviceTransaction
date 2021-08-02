using System.Threading.Tasks;
using SagaMicroserviceTransaction.Domain.Model;
using SagaMicroserviceTransaction.Domain.Model.Structs;

namespace SagaMicroserviceTransaction.Domain
{
    public interface IMicroserviceSaga
    {
        SagaId Id { get; }
        SagaStates State { get; }
        Task CompleteAsync();
        void Complete();
        Task RejectAsync(SagaException innerException = default);
        void Reject(SagaException innerException = default);
        void Initialize(SagaId id, SagaStates state);
        SagaId ResolveId(object message, IMicroserviceSagaContext context);
    }

    public interface IMicroserviceSaga<TData> : IMicroserviceSaga where TData : class
    {
        TData Data { get; }
        void Initialize(SagaId id, SagaStates states, TData data);
    }
}
