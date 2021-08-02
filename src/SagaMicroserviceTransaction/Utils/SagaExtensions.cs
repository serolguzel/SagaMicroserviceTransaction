using System;
using System.Linq;
using SagaMicroserviceTransaction.Domain;

namespace SagaMicroserviceTransaction.Utils
{
    public static class Extensions
    {
        public static Type GetSagaDataType(this IMicroserviceSaga saga)
            => saga
                .GetType()
                .GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMicroserviceSaga<>))
               ?.GetGenericArguments()
                .FirstOrDefault();
        
        public static object InvokeGeneric(this IMicroserviceSaga saga, string method, params object[] args)
            => saga
                .GetType()
                .GetMethod(method, args.Select(arg => arg.GetType()).ToArray())
                ?.Invoke(saga, args);
    }
}
