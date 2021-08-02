using System;
using Microsoft.Extensions.DependencyInjection;
using SagaMicroserviceTransaction.DependencyInjection;

namespace SagaMicroserviceTransactionTest
{
    public class DependencyInjection
    {
        public IServiceProvider ServiceProvider { get; set; }
        public DependencyInjection()
        {
            var services = new ServiceCollection();
            services.AddMicroserviceSaga();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}