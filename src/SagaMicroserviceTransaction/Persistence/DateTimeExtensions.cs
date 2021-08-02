using System;

namespace SagaMicroserviceTransaction.Persistence
{
    internal static class DateTimeExtensions
    {
        internal static long GetTimeStamp(this DateTimeOffset dateTime)
            => dateTime.ToUnixTimeMilliseconds();
    }
}
