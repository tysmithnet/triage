// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ClrMethodAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrMd = Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class ClrMethodAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrMethod" />
    internal class ClrMethodAdapter : BaseAdapter, IClrMethod
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrMethodAdapter" /> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <exception cref="ArgumentNullException">method</exception>
        /// <inheritdoc />
        public ClrMethodAdapter(IConverter converter, ClrMd.ClrMethod method) : base(converter)
        {
            Method = method ?? throw new ArgumentNullException(nameof(method));
            GCInfo = Method.GCInfo;
            IsAbstract = Method.IsAbstract;
            IsClassConstructor = Method.IsClassConstructor;
            IsConstructor = Method.IsConstructor;
            IsFinal = Method.IsFinal;
            IsInternal = Method.IsInternal;
            IsPInvoke = Method.IsPInvoke;
            IsPrivate = Method.IsPrivate;
            IsProtected = Method.IsProtected;
            IsPublic = Method.IsPublic;
            IsRTSpecialName = Method.IsRTSpecialName;
            IsSpecialName = Method.IsSpecialName;
            IsStatic = Method.IsStatic;
            IsVirtual = Method.IsVirtual;
            MetadataToken = Method.MetadataToken;
            try
            {
                MethodDesc = Method.MethodDesc;
            }
            catch (Exception)
            {
                ; // CLRMd bug
            }
            Name = Method.Name;
            NativeCode = Method.NativeCode;
        }

        /// <summary>
        ///     Enumerates all method descs for this method in the process.  MethodDescs
        ///     are unique to an Method/AppDomain pair, so when there are multiple domains
        ///     there will be multiple MethodDescs for a method.
        /// </summary>
        /// <returns>
        ///     An enumeration of method handles in the process for this given
        ///     method.
        /// </returns>
        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateMethodDescs() => Method.EnumerateMethodDescs();

        /// <summary>
        ///     Returns the full signature of the function.  For example, "void System.Foo.Bar(object o, int i)"
        ///     would return "System.Foo.Bar(System.Object, System.Int32)"
        /// </summary>
        /// <returns>System.String.</returns>
        /// <inheritdoc />
        public string GetFullSignature() => Method.GetFullSignature();

        /// <summary>
        ///     Gets the ILOffset of the given address within this method.
        /// </summary>
        /// <param name="addr">The absolute address of the code (not a relative offset).</param>
        /// <returns>The IL offset of the given address.</returns>
        /// <inheritdoc />
        public int GetILOffset(ulong addr) => Method.GetILOffset(addr);

        public override void Setup()
        {
            CompilationType = Converter.Convert(Method.CompilationType);
            HotColdInfo = Converter.Convert(Method.HotColdInfo);
            IlInfo = Converter.Convert(Method.IL);
            IlOffsetMap = Method.ILOffsetMap?.Select(Converter.Convert).ToArray();
            Type = Converter.Convert(Method.Type);
        }

        /// <summary>
        ///     Returns the way this method was compiled.
        /// </summary>
        /// <value>The type of the compilation.</value>
        /// <inheritdoc />
        public MethodCompilationType CompilationType { get; internal set; }

        /// <summary>
        ///     Returns the location of the GCInfo for this method.
        /// </summary>
        /// <value>The gc information.</value>
        /// <inheritdoc />
        public ulong GCInfo { get; internal set; }

        /// <summary>
        ///     Returns the regions of memory that
        /// </summary>
        /// <value>The hot cold information.</value>
        /// <inheritdoc />
        public IHotColdRegions HotColdInfo { get; internal set; }

        /// <summary>
        ///     Returns the location in memory of the IL for this method.
        /// </summary>
        /// <value>The il.</value>
        /// <inheritdoc />
        public IILInfo IlInfo { get; internal set; }

        /// <summary>
        ///     Returns the IL to native offset mapping.
        /// </summary>
        /// <value>The il offset map.</value>
        /// <inheritdoc />
        public ILToNativeMap[] IlOffsetMap { get; internal set; }

        /// <summary>
        ///     Returns if this method is abstract.
        /// </summary>
        /// <value><c>true</c> if this instance is abstract; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsAbstract { get; internal set; }

        /// <summary>
        ///     Returns whether this method is a static constructor.
        /// </summary>
        /// <value><c>true</c> if this instance is class constructor; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsClassConstructor { get; internal set; }

        /// <summary>
        ///     Returns whether this method is an instance constructor.
        /// </summary>
        /// <value><c>true</c> if this instance is constructor; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsConstructor { get; internal set; }

        /// <summary>
        ///     Returns if this method is final.
        /// </summary>
        /// <value><c>true</c> if this instance is final; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsFinal { get; internal set; }

        /// <summary>
        ///     Returns if this method is internal.
        /// </summary>
        /// <value><c>true</c> if this instance is internal; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsInternal { get; internal set; }

        /// <summary>
        ///     Returns if this method is a PInvoke.
        /// </summary>
        /// <value><c>true</c> if this instance is p invoke; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPInvoke { get; internal set; }

        /// <summary>
        ///     Returns if this method is private.
        /// </summary>
        /// <value><c>true</c> if this instance is private; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPrivate { get; internal set; }

        /// <summary>
        ///     Returns if this method is protected.
        /// </summary>
        /// <value><c>true</c> if this instance is protected; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsProtected { get; internal set; }

        /// <summary>
        ///     Returns if this method is public.
        /// </summary>
        /// <value><c>true</c> if this instance is public; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPublic { get; internal set; }

        /// <summary>
        ///     Returns if this method is runtime special method.
        /// </summary>
        /// <value><c>true</c> if this instance is rt special name; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsRTSpecialName { get; internal set; }

        /// <summary>
        ///     Returns if this method is a special method.
        /// </summary>
        /// <value><c>true</c> if this instance is special name; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsSpecialName { get; internal set; }

        /// <summary>
        ///     Returns if this method is static.
        /// </summary>
        /// <value><c>true</c> if this instance is static; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsStatic { get; internal set; }

        /// <summary>
        ///     Returns if this method is virtual.
        /// </summary>
        /// <value><c>true</c> if this instance is virtual; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsVirtual { get; internal set; }

        /// <summary>
        ///     Returns the metadata token of the current method.
        /// </summary>
        /// <value>The metadata token.</value>
        /// <inheritdoc />
        public uint MetadataToken { get; internal set; }

        /// <summary>
        ///     Retrieves the first MethodDesc in EnumerateMethodDescs().  For single
        ///     AppDomain programs this is the only MethodDesc.  MethodDescs
        ///     are unique to an Method/AppDomain pair, so when there are multiple domains
        ///     there will be multiple MethodDescs for a method.
        /// </summary>
        /// <value>The method desc.</value>
        /// <inheritdoc />
        public ulong MethodDesc { get; internal set; }

        /// <summary>
        ///     The name of the method.  For example, "void System.Foo.Bar(object o, int i)" would return "Bar".
        /// </summary>
        /// <value>The name.</value>
        /// <inheritdoc />
        public string Name { get; internal set; }

        /// <summary>
        ///     Returns the instruction pointer in the target process for the start of the method's assembly.
        /// </summary>
        /// <value>The native code.</value>
        /// <inheritdoc />
        public ulong NativeCode { get; internal set; }

        /// <summary>
        ///     Returns the enclosing type of this method.
        /// </summary>
        /// <value>The type.</value>
        /// <inheritdoc />
        public IClrType Type { get; internal set; }

        /// <summary>
        ///     Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        internal ClrMd.ClrMethod Method { get; set; }
    }
}