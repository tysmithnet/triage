using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrStaticFieldAdapter : IClrStaticField
    {
        /// <inheritdoc />
        public ClrStaticFieldAdapter(Microsoft.Diagnostics.Runtime.ClrStaticField staticField)
        {
            _staticField = staticField ?? throw new ArgumentNullException(nameof(staticField));
        }

        internal Microsoft.Diagnostics.Runtime.ClrStaticField _staticField;

        /// <inheritdoc />
        public ulong GetAddress(IClrAppDomain appDomain)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object GetDefaultValue()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object GetValue(IClrAppDomain appDomain)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object GetValue(IClrAppDomain appDomain, bool convertStrings)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool IsInitialized(IClrAppDomain appDomain)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ClrElementType ElementType { get; }

        /// <inheritdoc />
        public bool HasDefaultValue { get; }

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