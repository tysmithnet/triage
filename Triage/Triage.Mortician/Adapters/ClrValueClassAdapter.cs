using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrValueClassAdapter : IClrValueClass
    {
        /// <inheritdoc />
        public ClrValueClassAdapter(Microsoft.Diagnostics.Runtime.ClrValueClass valueClass)
        {
            _valueClass = valueClass;
        }

        internal Microsoft.Diagnostics.Runtime.ClrValueClass _valueClass;

        /// <inheritdoc />
        public T GetField<T>(string fieldName) where T : struct
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrObject GetObjectField(string fieldName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string GetStringField(string fieldName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrValueClass GetValueClassField(string fieldName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong Address { get; }

        /// <inheritdoc />
        public string HexAddress { get; }

        /// <inheritdoc />
        public IClrType Type { get; }
    }
}