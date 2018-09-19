using System;
using System.ComponentModel.Composition;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrThreadStaticFieldAdapter : IClrThreadStaticField
    {
        [Import]
        internal IConverter Converter { get; set; }
        /// <inheritdoc />
        public ClrThreadStaticFieldAdapter(Microsoft.Diagnostics.Runtime.ClrThreadStaticField threadStaticField)
        {
            ThreadStaticField = threadStaticField ?? throw new ArgumentNullException(nameof(threadStaticField));
            ElementType = Converter.Convert(threadStaticField.ElementType);
            Type = Converter.Convert(threadStaticField.Type);
        }

        internal Microsoft.Diagnostics.Runtime.ClrThreadStaticField ThreadStaticField;

        /// <inheritdoc />
        public ulong GetAddress(IClrAppDomain appDomain, IClrThread thread)
        {
            var convertedAppDomain = (appDomain as ClrAppDomainAdapter)?.AppDomain;
            var convertedThread = (thread as ClrThreadAdapter)?.Thread;
            return ThreadStaticField.GetAddress(convertedAppDomain, convertedThread);
        }

        /// <inheritdoc />
        public object GetValue(IClrAppDomain appDomain, IClrThread thread)
        {
            var convertedAppDomain = (appDomain as ClrAppDomainAdapter)?.AppDomain;
            var convertedThread = (thread as ClrThreadAdapter)?.Thread;
            return ThreadStaticField.GetValue(convertedAppDomain, convertedThread);
        }

        /// <inheritdoc />
        public object GetValue(IClrAppDomain appDomain, IClrThread thread, bool convertStrings)
        {
            var convertedAppDomain = (appDomain as ClrAppDomainAdapter)?.AppDomain;
            var convertedThread = (thread as ClrThreadAdapter)?.Thread;
            return ThreadStaticField.GetValue(convertedAppDomain, convertedThread, convertStrings);
        }

        /// <inheritdoc />
        public ClrElementType ElementType { get; }

        /// <inheritdoc />
        public bool HasSimpleValue => ThreadStaticField.HasSimpleValue;

        /// <inheritdoc />
        public bool IsInternal => ThreadStaticField.IsInternal;

        /// <inheritdoc />
        public bool IsObjectReference => ThreadStaticField.IsObjectReference;

        /// <inheritdoc />
        public bool IsPrimitive => ThreadStaticField.IsPrimitive;

        /// <inheritdoc />
        public bool IsPrivate => ThreadStaticField.IsPrivate;

        /// <inheritdoc />
        public bool IsProtected => ThreadStaticField.IsProtected;

        /// <inheritdoc />
        public bool IsPublic => ThreadStaticField.IsPublic;

        /// <inheritdoc />
        public bool IsValueClass => ThreadStaticField.IsValueClass;

        /// <inheritdoc />
        public string Name => ThreadStaticField.Name;

        /// <inheritdoc />
        public int Offset => ThreadStaticField.Offset;

        /// <inheritdoc />
        public int Size => ThreadStaticField.Size;

        /// <inheritdoc />
        public uint Token => ThreadStaticField.Token;

        /// <inheritdoc />
        public IClrType Type { get; }
    }
}