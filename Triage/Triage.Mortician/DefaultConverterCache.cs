using System;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;

namespace Triage.Mortician
{
    [Export(typeof(IConverterCache))]
    internal class DefaultConverterCache : IConverterCache
    {
        /// <inheritdoc />
        public bool Contains(object instance)
        {
            lock (WeakMap)
            {
                return WeakMap.TryGetValue(instance, out var throwAway);
            }
        }

        /// <inheritdoc />
        public T GetOrAdd<T>(object instance, Func<T> factoryMethod, Action setupFunction = null)
        {
            if (instance == null)
                return default(T);

            lock (WeakMap)
            {
                if (WeakMap.TryGetValue(instance, out var value))
                {
                    return (T) value;
                }

                var created = factoryMethod();
                WeakMap.Add(instance, created);
                setupFunction?.Invoke();
                return created;
            }
        }

        private ConditionalWeakTable<object, object> WeakMap { get; } = new ConditionalWeakTable<object, object>();
    }
}