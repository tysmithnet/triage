// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-24-2018
// ***********************************************************************
// <copyright file="DefaultConverterCache.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;

namespace Triage.Mortician
{
    /// <summary>
    ///     Class DefaultConverterCache.
    /// </summary>
    /// <seealso cref="Triage.Mortician.IConverterCache" />
    [Export(typeof(IConverterCache))]
    internal class DefaultConverterCache : IConverterCache
    {
        /// <summary>
        ///     Determines whether [contains] [the specified instance].
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns><c>true</c> if [contains] [the specified instance]; otherwise, <c>false</c>.</returns>
        /// <inheritdoc />
        public bool Contains(object instance)
        {
            lock (WeakMap)
            {
                return WeakMap.TryGetValue(instance, out var throwAway);
            }
        }

        /// <summary>
        ///     Gets the or add.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="factoryMethod">The factory method.</param>
        /// <param name="setupFunction">The setup function.</param>
        /// <returns>T.</returns>
        /// <inheritdoc />
        public T GetOrAdd<T>(object instance, Func<T> factoryMethod, Action setupFunction = null)
        {
            if (instance == null)
                return default(T);

            lock (WeakMap)
            {
                if (WeakMap.TryGetValue(instance, out var value)) return (T) value;

                var created = factoryMethod();
                WeakMap.Add(instance, created);
                setupFunction?.Invoke();
                return created;
            }
        }

        /// <summary>
        ///     Gets the weak map.
        /// </summary>
        /// <value>The weak map.</value>
        private ConditionalWeakTable<object, object> WeakMap { get; } = new ConditionalWeakTable<object, object>();
    }
}