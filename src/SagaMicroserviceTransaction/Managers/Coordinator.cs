using AsyncKeyedLock;
using SagaMicroserviceTransaction.Domain;
using SagaMicroserviceTransaction.Domain.Managers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SagaMicroserviceTransaction.Managers
{
    public class Coordinator : ICoordinator
    {
        private readonly ISearcher seeker;
        private readonly IInitializer initializer;
        private readonly IProcessor processor;
        private readonly IPostProcessor postProcessor;
        private static readonly AsyncKeyedLocker<string> asyncKeyedLocker = new AsyncKeyedLocker<string>(o =>
        {
            o.PoolSize = 20;
            o.PoolInitialFill = 1;
        });

        public Coordinator(ISearcher seeker, IInitializer initializer, IProcessor processor,
            IPostProcessor postProcessor)
        {
            this.seeker = seeker;
            this.initializer = initializer;
            this.processor = processor;
            this.postProcessor = postProcessor;
        }

        public Task ProcessAsync<TData, TMessage>(TMessage message, IMicroserviceSagaContext context = null) where TMessage : class
            => ProcessAsync<TData, TMessage>(message, null, null, context);

        public async Task ProcessAsync<TData, TMessage>(TMessage message, Func<TMessage, IMicroserviceSagaContext, Task> onCompleted = null,
            Func<TMessage, IMicroserviceSagaContext, Task> onRejected = null, IMicroserviceSagaContext context = null) where TMessage : class
        {
            var actions = seeker.Search<TMessage>().ToList();

            static Task EmptyHook(TMessage m, IMicroserviceSagaContext ctx) => Task.CompletedTask;
            onCompleted ??= EmptyHook;
            onRejected ??= EmptyHook;

            var sagaTasks = actions
                .Select(action => ProcessAsync<TData, TMessage>(message, action, onCompleted, onRejected, context))
                .ToList();

            await Task.WhenAll(sagaTasks);
        }

        private async Task ProcessAsync<TData, TMessage>(TMessage message, ISagaMethod<TMessage> action,
            Func<TMessage, IMicroserviceSagaContext, Task> onCompleted, Func<TMessage, IMicroserviceSagaContext, Task> onRejected,
            IMicroserviceSagaContext context = null) where TMessage : class
        {
            context ??= MicroserviceSagaContext.Empty;
            var saga = (IMicroserviceSaga)action;
            var id = saga.ResolveId(message, context);

            using (await asyncKeyedLocker.LockAsync(id).ConfigureAwait(false))
            {
                var (isInitialized, state) = await initializer.TryInitializeAsync<TData, TMessage>(saga, id, message);

                if (!isInitialized)
                {
                    return;
                }

                await processor.ProcessAsync<TData, TMessage>(saga, message, state, context);
                await postProcessor.ProcessAsync<TData, TMessage>(saga, message, context, onCompleted, onRejected);
            }
        }
    }
}
