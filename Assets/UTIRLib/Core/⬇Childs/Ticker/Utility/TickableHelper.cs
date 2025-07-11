using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using UnityEngine;
using UTIRLib.Extensions;
using UTIRLib.UExtensions;

#nullable enable

namespace UTIRLib.TickerX
{
    public static class TickableHelper
    {
        public static bool HasCustomTickableAttribute(Type? type)
        {
            if (type == null)
            {
                return false;
            }

            return type.IsDefined(typeof(CustomTickableAttribute));
        }

        public static bool HasCustomTickableAttribute<T>() => HasCustomTickableAttribute(typeof(T));

        public static bool HasCustomTickableAttribute(ITickableBase tickable) =>
            HasCustomTickableAttribute(tickable?.GetType());

        /// <exception cref="ArgumentNullException"></exception>
        public static CustomTickableAttribute GetCustomTickableAttribute(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.GetCustomAttribute<CustomTickableAttribute>();
        }

        public static CustomTickableAttribute GetCustomTickableAttribute<T>() => GetCustomTickableAttribute(typeof(T));

        public static CustomTickableAttribute GetCustomTickableAttribute(ITickableBase tickable) =>
            GetCustomTickableAttribute(tickable.GetType());

        public static bool TryGetCustomTickableAttribute(Type type,
            [NotNullWhen(true)] out CustomTickableAttribute? useCustomTickerAttribute)
        {
            useCustomTickerAttribute = GetCustomTickableAttribute(type);

            return useCustomTickerAttribute is not null;
        }

        public static bool TryGetCustomTickableAttribute<T>(
            [NotNullWhen(true)] out CustomTickableAttribute? useCustomTickerAttribute)
        {
            useCustomTickerAttribute = GetCustomTickableAttribute<T>();

            return useCustomTickerAttribute is not null;
        }

        public static bool TryGetCustomTickableAttribute(ITickableBase tickable,
            [NotNullWhen(true)] out CustomTickableAttribute? useCustomTickerAttribute)
        {
            useCustomTickerAttribute = GetCustomTickableAttribute(tickable);

            return useCustomTickerAttribute is not null;
        }

        public static bool IsTickable(Type? type) => type != null && typeof(ITickableBase).IsAssignableFrom(type);

        public static bool IsTickable<T>() => IsTickable(typeof(T));

        public static bool IsTickable(object? obj) => IsTickable(obj?.GetType());

        public static bool IsTickable(GameObject gameObject)
        {
            if (gameObject.TryGetAssignedObject<ITickableBase>(out _))
            {
                return true;
            }

            return false;
        }

        /// <exception cref="ArgumentNullException"></exception>
        public static Type[] GetTickableInterfaces(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Type[] interfaceTypes = type.GetInterfaces();
            Type tickableBaseInterfaceType = typeof(ITickableBase);
            List<Type> tickableInterfaces = new();
            for (int i = 0; i < interfaceTypes.Length; i++)
            {
                if (tickableBaseInterfaceType.IsAssignableFrom(interfaceTypes[i]))
                {
                    tickableInterfaces.Add(interfaceTypes[i]);
                }
            }

            return tickableInterfaces.ToArray();
        }

        public static Type[] GetTickableInterfaces<T>() => GetTickableInterfaces(typeof(T));

        public static Type[] GetTickableInterfaces(ITickableBase tickable) =>
            GetTickableInterfaces(tickable.GetType());

        public static bool TryGetTickableInterfaces(Type type, out Type[] tickableInterfaces)
        {
            tickableInterfaces = GetTickableInterfaces(type);

            return tickableInterfaces.Length > 0;
        }

        public static bool TryGetTickableInterfaces<T>(out Type[] tickableInterfaces)
        {
            tickableInterfaces = GetTickableInterfaces<T>();

            return tickableInterfaces.Length > 0;
        }

        public static bool TryGetTickableInterfaces(ITickableBase tickable, out Type[] tickableInterfaces)
        {
            tickableInterfaces = GetTickableInterfaces(tickable);

            return tickableInterfaces.Length > 0;
        }
    }
}