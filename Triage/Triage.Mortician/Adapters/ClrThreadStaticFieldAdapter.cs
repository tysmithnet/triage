using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrThreadStaticFieldAdapter : IClrThreadStaticField
    {
        /// <inheritdoc />
        public ClrThreadStaticFieldAdapter(Microsoft.Diagnostics.Runtime.ClrThreadStaticField threadStaticField)
        {
            _threadStaticField = threadStaticField ?? throw new ArgumentNullException(nameof(threadStaticField));
        }

        internal Microsoft.Diagnostics.Runtime.ClrThreadStaticField _threadStaticField;

        /// <inheritdoc />
        public ulong GetAddress(IClrAppDomain appDomain, IClrThread thread)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object GetValue(IClrAppDomain appDomain, IClrThread thread)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object GetValue(IClrAppDomain appDomain, IClrThread thread, bool convertStrings)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ClrElementType ElementType { get; }

        /// <inheritdoc />
        public bool HasSimpleValue { get; }

        /// <inheritdoc />
        public bool IsInternal { get; }

        /// <inheritdoc />
        public bool IsObjectReference { get; }

        /// <inheritdoc />
        public bool IsPrimitive { get; }

        /// <inheritdoc />
        public bool IsPrivate { get; }

        /// <inheritdoc />
        public bool IsProtected { get; }

        /// <inheritdoc />
        public bool IsPublic { get; }

        /// <inheritdoc />
        public bool IsValueClass { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public int Offset { get; }

        /// <inheritdoc />
        public int Size { get; }

        /// <inheritdoc />
        public uint Token { get; }

        /// <inheritdoc />
        public IClrType Type { get; }
    }
}