using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SagaMicroserviceTransaction.Domain;
using SagaMicroserviceTransaction.Domain.Managers;
using SagaMicroserviceTransaction.Managers;

namespace SagaMicroserviceTransaction.DependencyInjection
{
    public static class Extensions
    {
        public static IServiceCollection AddMicroserviceSaga(this IServiceCollection services, Action<IMicroserviceSagaBuilder> build = null)
        {
            services.AddTransient<ICoordinator, Coordinator>();
            services.AddTransient<ISearcher, Searcher>();
            services.AddTransient<IInitializer, Initializer>();
            services.AddTransient<IProcessor, Processor>();
            services.AddTransient<IPostProcessor, PostProcessor>();

            var builder = new MicroserviceSagaBuilder(services);

            if (build is null)
                builder.UseInMemoryPersistence();
            else
                build(builder);

            services.RegisterSagas();

            return services;
        }

        private static void RegisterSagas(this IServiceCollection services)
            => services.Scan(scan =>
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();

                scan
                    .FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo(typeof(IMicroserviceSaga)))
                    .As(t => t
                        .GetTypeInfo()
                        .GetInterfaces(includeInherited: false))
                    .WithTransientLifetime();
            });

        private static IEnumerable<Type> GetInterfaces(this Type type, bool includeInherited)
        {
            if (includeInherited || type.BaseType is null)
                return type.GetInterfaces();
            
            return type.GetInterfaces().Except(type.BaseType.GetInterfaces());
        }
    }
}
