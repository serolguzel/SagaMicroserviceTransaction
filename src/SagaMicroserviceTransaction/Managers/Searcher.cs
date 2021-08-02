using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SagaMicroserviceTransaction.Domain;
using SagaMicroserviceTransaction.Domain.Managers;

namespace SagaMicroserviceTransaction.Managers
{
    public class Searcher : ISearcher
    {
        private readonly IServiceProvider serviceProvider;

        public Searcher(IServiceProvider serviceProvider)
            => this.serviceProvider = serviceProvider;

        public IEnumerable<ISagaMethod<TMessage>> Search<TMessage>()
            => serviceProvider.GetService<IEnumerable<ISagaMethod<TMessage>>>()
            .Union(serviceProvider.GetService<IEnumerable<ISagaBeginMethod<TMessage>>>())
            .GroupBy(s => s.GetType())
            .Select(g => g.First())
            .Distinct();
    }
}
