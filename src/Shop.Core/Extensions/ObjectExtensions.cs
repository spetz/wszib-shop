using System;

namespace Shop.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static T FailIfNull<T>(this T value, string exceptionMessage) where T : class
        {
            if (value == null)
            {
                throw new Exception(exceptionMessage);
            }

            return value;
        }

        public static void FailIfExists<T>(this T value, string exceptionMessage) where T : class
        {
            if (value != null)
            {
                throw new Exception(exceptionMessage);
            }
        }
    }
}
