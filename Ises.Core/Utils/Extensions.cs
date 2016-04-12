using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ises.Core.Utils
{
    public static class Extensions
    {
        public static bool Implements<T>(this Type objectType)
        {
            return objectType.Implements(typeof(T));
        }

        private static bool Implements(this object obj, Type interfaceType)
        {
            var objectType = obj.GetType();

            return objectType.Implements(interfaceType);
        }

        private static bool Implements(this Type objectType, Type interfaceType)
        {
            if (interfaceType.IsGenericTypeDefinition)
                return objectType.ImplementsGeneric(interfaceType);

            return interfaceType.IsAssignableFrom(objectType);
        }

        private static bool ImplementsGeneric(this Type objectType, Type interfaceType)
        {
            Type matchedType;
            return objectType.ImplementsGeneric(interfaceType, out matchedType);
        }

        private static bool ImplementsGeneric(this Type objectType, Type interfaceType, out Type matchedType)
        {
            matchedType = null;

            if (interfaceType.IsInterface)
            {
                matchedType = objectType.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == interfaceType);
                if (matchedType != null)
                    return true;
            }

            if (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == interfaceType)
            {
                matchedType = objectType;
                return true;
            }

            var baseType = objectType.BaseType;
            if (baseType == null)
                return false;

            return baseType.ImplementsGeneric(interfaceType, out matchedType);
        }

        public static void Each<T>(this IEnumerable<T> ts, Action<T> action)
        {
            foreach (var t in ts)
            {
                action(t);
            }
        }

        public static string GetInformationalVersion(this Assembly assembly)
        {
            return assembly
                .GetCustomAttributes(false)
                .OfType<AssemblyInformationalVersionAttribute>()
                .Single()
                .InformationalVersion;
        }
    }
}
