using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrMd = Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician.Adapters
{
    internal class ClrMethodAdapter : IClrMethod
    {
        internal ClrMd.ClrMethod Method { get; set; }

        /// <inheritdoc />
        public ClrMethodAdapter(ClrMd.ClrMethod method)
        {
            Method = method ?? throw new ArgumentNullException(nameof(method));
            CompilationType = Converter.Convert(Method.CompilationType);
            HotColdInfo = Converter.Convert(Method.HotColdInfo);
            IlInfo = Converter.Convert(Method.IL);
            IlOffsetMap = Method.ILOffsetMap.Select(Converter.Convert).ToArray();
            Type = Converter.Convert(Method.Type);
        }

        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateMethodDescs()
        {
            return Method.EnumerateMethodDescs();
        }

        /// <inheritdoc />
        public string GetFullSignature()
        {
            return Method.GetFullSignature();
        }

        /// <inheritdoc />
        public int GetILOffset(ulong addr)
        {
            return Method.GetILOffset(addr);
        }

        /// <inheritdoc />
        public MethodCompilationType CompilationType { get; }

        /// <inheritdoc />
        public ulong GCInfo => Method.GCInfo;

        /// <inheritdoc />
        public IHotColdRegions HotColdInfo { get; }

        /// <inheritdoc />
        public IILInfo IlInfo { get; }

        /// <inheritdoc />
        public ILToNativeMap[] IlOffsetMap { get; }

        /// <inheritdoc />
        public bool IsAbstract => Method.IsAbstract;

        /// <inheritdoc />
        public bool IsClassConstructor => Method.IsClassConstructor;

        /// <inheritdoc />
        public bool IsConstructor => Method.IsConstructor;

        /// <inheritdoc />
        public bool IsFinal => Method.IsFinal;

        /// <inheritdoc />
        public bool IsInternal => Method.IsInternal;

        /// <inheritdoc />
        public bool IsPInvoke => Method.IsPInvoke;

        /// <inheritdoc />
        public bool IsPrivate => Method.IsPrivate;

        /// <inheritdoc />
        public bool IsProtected => Method.IsProtected;

        /// <inheritdoc />
        public bool IsPublic => Method.IsPublic;

        /// <inheritdoc />
        public bool IsRTSpecialName => Method.IsRTSpecialName;

        /// <inheritdoc />
        public bool IsSpecialName => Method.IsSpecialName;

        /// <inheritdoc />
        public bool IsStatic => Method.IsStatic;

        /// <inheritdoc />
        public bool IsVirtual => Method.IsVirtual;

        /// <inheritdoc />
        public uint MetadataToken => Method.MetadataToken;

        /// <inheritdoc />
        public ulong MethodDesc => Method.MethodDesc;

        /// <inheritdoc />
        public string Name => Method.Name;

        /// <inheritdoc />
        public ulong NativeCode => Method.NativeCode;

        /// <inheritdoc />
        public IClrType Type { get; }
    }
}
