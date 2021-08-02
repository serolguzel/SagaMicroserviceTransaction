using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SagaMicroserviceTransaction.Domain;
using SagaMicroserviceTransaction.Domain.Model;
using SagaMicroserviceTransaction.Domain.Model.Structs;

namespace SagaMicroserviceTransaction
{
    public abstract class MicroserviceSaga : IMicroserviceSaga
    {
        public ValidationProblemDetails ProblemDetails { get; set; }
        public SagaId Id { get; private set; }

        public SagaStates State { get; private set; }

        public virtual void Initialize(SagaId id, SagaStates state)
            => (Id, State) = (id, state);

        public virtual SagaId ResolveId(object message, IMicroserviceSagaContext context)
            => context.SagaId;

        public virtual void Complete()
            => State = SagaStates.Completed;

        public virtual Task CompleteAsync()
        {
            Complete();
            return Task.CompletedTask;
        }
        public virtual void Reject(SagaException innerException = null)
        {
            State = SagaStates.Rejected;
            if (innerException == null) throw new SagaException(ProblemDetails);
            ProblemDetails = innerException.ProblemDetails;
            throw innerException;
        }

        public virtual Task RejectAsync(SagaException innerException = null)
        {
            Reject(innerException);
            return Task.CompletedTask;
        }
    }

    public abstract class MicroserviceSaga<TData> : MicroserviceSaga, IMicroserviceSaga<TData> where TData : class, new()
    {
        public TData Data { get; private set; }

        public virtual void Initialize(SagaId id, SagaStates state, TData data)
        {
            base.Initialize(id, state);
            Data = data;
        }
    }
}
