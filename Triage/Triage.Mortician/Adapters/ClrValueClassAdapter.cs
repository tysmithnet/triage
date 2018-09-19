using System;
using System.ComponentModel.Composition;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrValueClassAdapter : IClrValueClass
    {
        [Import]
        internal IConverter Converter { get; set; }
        /// <inheritdoc />
        public ClrValueClassAdapter(Microsoft.Diagnostics.Runtime.ClrValueClass valueClass)
        {
            ValueClass = valueClass;
            Type = Converter.Convert(valueClass.Type);
        }

        internal Microsoft.Diagnostics.Runtime.ClrValueClass ValueClass;

        /// <inheritdoc />
        public T GetField<T>(string fieldName) where T : struct => ValueClass.GetField<T>(fieldName);

        /// <inheritdoc />
        public IClrObject GetObjectField(string fieldName) => Converter.Convert(ValueClass.GetObjectField(fieldName));

        /// <inheritdoc />
        public string GetStringField(string fieldName) => ValueClass.GetStringField(fieldName);

        /// <inheritdoc />
        public IClrValueClass GetValueClassField(string fieldName) => Converter.Convert(ValueClass.GetValueClassField(fieldName));

        /// <inheritdoc />
        public ulong Address => ValueClass.Address;

        /// <inheritdoc />
        public string HexAddress => ValueClass.HexAddress;

        /// <inheritdoc />
        public IClrType Type { get; }
    }
}