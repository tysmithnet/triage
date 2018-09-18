using System;
using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrObjectAdapter : IClrObject
    {
        /// <inheritdoc />
        public ClrObjectAdapter(Microsoft.Diagnostics.Runtime.ClrObject o)
        {
            _object = o;
        }

        internal Microsoft.Diagnostics.Runtime.ClrObject _object;

        /// <inheritdoc />
        public IEnumerable<IClrObject> EnumerateObjectReferences(bool carefully = false)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool Equals(IClrObject other)
        {
            throw new NotImplementedException();
        }

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
        public bool ContainsPointers { get; }

        /// <inheritdoc />
        public string HexAddress { get; }

        /// <inheritdoc />
        public bool IsArray { get; }

        /// <inheritdoc />
        public bool IsBoxed { get; }

        /// <inheritdoc />
        public bool IsNull { get; }

        /// <inheritdoc />
        public int Length { get; }

        /// <inheritdoc />
        public ulong Size { get; }

        /// <inheritdoc />
        public IClrType Type { get; }
    }
}