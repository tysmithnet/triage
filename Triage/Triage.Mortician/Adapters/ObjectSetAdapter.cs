using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ObjectSetAdapter : IObjectSet
    {
        /// <inheritdoc />
        public ObjectSetAdapter(Microsoft.Diagnostics.Runtime.ObjectSet objectSet)
        {
            ObjectSet = objectSet ?? throw new ArgumentNullException(nameof(objectSet));
        }

        internal Microsoft.Diagnostics.Runtime.ObjectSet ObjectSet;

        /// <inheritdoc />
        public bool Add(ulong obj) => ObjectSet.Add(obj);

        /// <inheritdoc />
        public void Clear() => ObjectSet.Clear();

        /// <inheritdoc />
        public bool Contains(ulong obj) => ObjectSet.Contains(obj);

        /// <inheritdoc />
        public bool Remove(ulong obj) => ObjectSet.Remove(obj);

        /// <inheritdoc />
        public int Count => ObjectSet.Count;
    }
}