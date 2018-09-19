using System;
using System.ComponentModel.Composition;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrStaticFieldAdapter : IClrStaticField
    {
        [Import]
        internal IConverter Converter { get; set; }
        /// <inheritdoc />
        public ClrStaticFieldAdapter(Microsoft.Diagnostics.Runtime.ClrStaticField staticField)
        {
            StaticField = staticField ?? throw new ArgumentNullException(nameof(staticField));
            ElementType = Converter.Convert(staticField.ElementType);
            Type = Converter.Convert(staticField.Type);
        }

        internal Microsoft.Diagnostics.Runtime.ClrStaticField StaticField;

        /// <inheritdoc />
        public ulong GetAddress(IClrAppDomain appDomain) => StaticField.GetAddress((appDomain as ClrAppDomainAdapter)?.AppDomain);


        /// <inheritdoc />
        public object GetDefaultValue() => StaticField.GetDefaultValue();


        /// <inheritdoc />
        public object GetValue(IClrAppDomain appDomain) => StaticField.GetValue((appDomain as ClrAppDomainAdapter)?.AppDomain);


        /// <inheritdoc />
        public object GetValue(IClrAppDomain appDomain, bool convertStrings) => StaticField.GetValue((appDomain as ClrAppDomainAdapter)?.AppDomain, convertStrings);


        /// <inheritdoc />
        public bool IsInitialized(IClrAppDomain appDomain) => StaticField.IsInitialized((appDomain as ClrAppDomainAdapter)?.AppDomain);


        /// <inheritdoc />
        public ClrElementType ElementType { get; }

        /// <inheritdoc />
        public bool HasDefaultValue => StaticField.HasDefaultValue;

        /// <inheritdoc />
        public bool HasSimpleValue => StaticField.HasSimpleValue;

        /// <inheritdoc />
        public bool IsInternal => StaticField.IsInternal;

        /// <inheritdoc />
        public bool IsObjectReference => StaticField.IsObjectReference;

        /// <inheritdoc />
        public bool IsPrimitive => StaticField.IsPrimitive;

        /// <inheritdoc />
        public bool IsPrivate => StaticField.IsPrivate;

        /// <inheritdoc />
        public bool IsProtected => StaticField.IsProtected;

        /// <inheritdoc />
        public bool IsPublic => StaticField.IsPublic;

        /// <inheritdoc />
        public bool IsValueClass => StaticField.IsValueClass;

        /// <inheritdoc />
        public string Name => StaticField.Name;

        /// <inheritdoc />
        public int Offset => StaticField.Offset;

        /// <inheritdoc />
        public int Size => StaticField.Size;

        /// <inheritdoc />
        public uint Token => StaticField.Token;

        /// <inheritdoc />
        public IClrType Type { get; }
    }
}