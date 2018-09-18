// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IClrMethod.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IClrMethod
    /// </summary>
    public interface IClrMethod
    {
        /// <summary>
        ///     Enumerates all method descs for this method in the process.  MethodDescs
        ///     are unique to an Method/AppDomain pair, so when there are multiple domains
        ///     there will be multiple MethodDescs for a method.
        /// </summary>
        /// <returns>
        ///     An enumeration of method handles in the process for this given
        ///     method.
        /// </returns>
        IEnumerable<ulong> EnumerateMethodDescs();

        /// <summary>
        ///     Returns the full signature of the function.  For example, "void System.Foo.Bar(object o, int i)"
        ///     would return "System.Foo.Bar(System.Object, System.Int32)"
        /// </summary>
        /// <returns>System.String.</returns>
        string GetFullSignature();

        /// <summary>
        ///     Gets the ILOffset of the given address within this method.
        /// </summary>
        /// <param name="addr">The absolute address of the code (not a relative offset).</param>
        /// <returns>The IL offset of the given address.</returns>
        int GetILOffset(ulong addr);

        /// <summary>
        ///     Returns the way this method was compiled.
        /// </summary>
        /// <value>The type of the compilation.</value>
        MethodCompilationType CompilationType { get; }

        /// <summary>
        ///     Returns the location of the GCInfo for this method.
        /// </summary>
        /// <value>The gc information.</value>
        ulong GCInfo { get; }

        /// <summary>
        ///     Returns the regions of memory that
        /// </summary>
        /// <value>The hot cold information.</value>
        IHotColdRegions HotColdInfo { get; }

        /// <summary>
        ///     Returns the location in memory of the IL for this method.
        /// </summary>
        /// <value>The il.</value>
        IILInfo IL { get; }

        /// <summary>
        ///     Returns the IL to native offset mapping.
        /// </summary>
        /// <value>The il offset map.</value>
        ILToNativeMap[] ILOffsetMap { get; }

        /// <summary>
        ///     Returns if this method is abstract.
        /// </summary>
        /// <value><c>true</c> if this instance is abstract; otherwise, <c>false</c>.</value>
        bool IsAbstract { get; }

        /// <summary>
        ///     Returns whether this method is a static constructor.
        /// </summary>
        /// <value><c>true</c> if this instance is class constructor; otherwise, <c>false</c>.</value>
        bool IsClassConstructor { get; }

        /// <summary>
        ///     Returns whether this method is an instance constructor.
        /// </summary>
        /// <value><c>true</c> if this instance is constructor; otherwise, <c>false</c>.</value>
        bool IsConstructor { get; }

        /// <summary>
        ///     Returns if this method is final.
        /// </summary>
        /// <value><c>true</c> if this instance is final; otherwise, <c>false</c>.</value>
        bool IsFinal { get; }

        /// <summary>
        ///     Returns if this method is internal.
        /// </summary>
        /// <value><c>true</c> if this instance is internal; otherwise, <c>false</c>.</value>
        bool IsInternal { get; }

        /// <summary>
        ///     Returns if this method is a PInvoke.
        /// </summary>
        /// <value><c>true</c> if this instance is p invoke; otherwise, <c>false</c>.</value>
        bool IsPInvoke { get; }

        /// <summary>
        ///     Returns if this method is private.
        /// </summary>
        /// <value><c>true</c> if this instance is private; otherwise, <c>false</c>.</value>
        bool IsPrivate { get; }

        /// <summary>
        ///     Returns if this method is protected.
        /// </summary>
        /// <value><c>true</c> if this instance is protected; otherwise, <c>false</c>.</value>
        bool IsProtected { get; }

        /// <summary>
        ///     Returns if this method is public.
        /// </summary>
        /// <value><c>true</c> if this instance is public; otherwise, <c>false</c>.</value>
        bool IsPublic { get; }

        /// <summary>
        ///     Returns if this method is runtime special method.
        /// </summary>
        /// <value><c>true</c> if this instance is rt special name; otherwise, <c>false</c>.</value>
        bool IsRTSpecialName { get; }

        /// <summary>
        ///     Returns if this method is a special method.
        /// </summary>
        /// <value><c>true</c> if this instance is special name; otherwise, <c>false</c>.</value>
        bool IsSpecialName { get; }

        /// <summary>
        ///     Returns if this method is static.
        /// </summary>
        /// <value><c>true</c> if this instance is static; otherwise, <c>false</c>.</value>
        bool IsStatic { get; }

        /// <summary>
        ///     Returns if this method is virtual.
        /// </summary>
        /// <value><c>true</c> if this instance is virtual; otherwise, <c>false</c>.</value>
        bool IsVirtual { get; }

        /// <summary>
        ///     Returns the metadata token of the current method.
        /// </summary>
        /// <value>The metadata token.</value>
        uint MetadataToken { get; }

        /// <summary>
        ///     Retrieves the first MethodDesc in EnumerateMethodDescs().  For single
        ///     AppDomain programs this is the only MethodDesc.  MethodDescs
        ///     are unique to an Method/AppDomain pair, so when there are multiple domains
        ///     there will be multiple MethodDescs for a method.
        /// </summary>
        /// <value>The method desc.</value>
        ulong MethodDesc { get; }

        /// <summary>
        ///     The name of the method.  For example, "void System.Foo.Bar(object o, int i)" would return "Bar".
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        ///     Returns the instruction pointer in the target process for the start of the method's assembly.
        /// </summary>
        /// <value>The native code.</value>
        ulong NativeCode { get; }

        /// <summary>
        ///     Returns the enclosing type of this method.
        /// </summary>
        /// <value>The type.</value>
        IClrType Type { get; }
    }
}