using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ObjectSetAdapter : IObjectSet
    {
        /// <inheritdoc />
        public ObjectSetAdapter(Microsoft.Diagnostics.Runtime.ObjectSet objectSet)
        {
            _objectSet = objectSet ?? throw new ArgumentNullException(nameof(objectSet));
        }

        internal Microsoft.Diagnostics.Runtime.ObjectSet _objectSet;

        /// <inheritdoc />
        public bool Add(ulong obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool Contains(ulong obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool Remove(ulong obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int Count { get; }
    }
}