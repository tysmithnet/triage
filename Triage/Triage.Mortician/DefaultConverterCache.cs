using System;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;
using Triage.Mortician.Adapters;

namespace Triage.Mortician
{
    [Export(typeof(IConverterCache))]
    internal class DefaultConverterCache : IConverterCache
    {
        private ConditionalWeakTable<object, object> WeakMap { get; set; }= new ConditionalWeakTable<object, object>();

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
                else
                {
                    var created = factoryMethod();
                    WeakMap.Add(instance, created);
                    setupFunction?.Invoke();
                    return created;
                }
            }
        }

        /// <inheritdoc />
        public bool Contains(object instance)
        {
            lock (WeakMap)
            {
                return WeakMap.TryGetValue(instance, out var throwAway);
            }
        }
    }
}