// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ClrThreadStaticFieldAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Microsoft.Diagnostics.Runtime;
using Mortician.Core.ClrMdAbstractions;
using ClrElementType = Mortician.Core.ClrMdAbstractions.ClrElementType;

namespace Mortician.Adapters
{
    /// <summary>
    ///     Class ClrThreadStaticFieldAdapter.
    /// </summary>
    /// <seealso cref="Mortician.Core.ClrMdAbstractions.IClrThreadStaticField" />
    internal class ClrThreadStaticFieldAdapter : BaseAdapter, IClrThreadStaticField
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrThreadStaticFieldAdapter" /> class.
        /// </summary>
        /// <param name="threadStaticField">The thread static field.</param>
        /// <exception cref="ArgumentNullException">threadStaticField</exception>
        /// <inheritdoc />
        public ClrThreadStaticFieldAdapter(IConverter converter, ClrThreadStaticField threadStaticField) :
            base(converter)
        {
            ThreadStaticField = threadStaticField ?? throw new ArgumentNullException(nameof(threadStaticField));
            HasSimpleValue = ThreadStaticField.HasSimpleValue;
            IsInternal = ThreadStaticField.IsInternal;
            IsObjectReference = ThreadStaticField.IsObjectReference;
            IsPrimitive = ThreadStaticField.IsPrimitive;
            IsPrivate = ThreadStaticField.IsPrivate;
            IsProtected = ThreadStaticField.IsProtected;
            IsPublic = ThreadStaticField.IsPublic;
            IsValueClass = ThreadStaticField.IsValueClass;
            Name = ThreadStaticField.Name;
            Offset = ThreadStaticField.Offset;
            Size = ThreadStaticField.Size;
            Token = ThreadStaticField.Token;
        }

        /// <summary>
        ///     The thread static field
        /// </summary>
        internal ClrThreadStaticField ThreadStaticField;

        /// <summary>
        ///     Gets the address of the field.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the field's address.</param>
        /// <param name="thread">The thread on which to get the field's address.</param>
        /// <returns>The address of the field.</returns>
        /// <inheritdoc />
        public ulong GetAddress(IClrAppDomain appDomain, IClrThread thread)
        {
            var convertedAppDomain = (appDomain as ClrAppDomainAdapter)?.AppDomain;
            var convertedThread = (thread as ClrThreadAdapter)?.Thread;
            return ThreadStaticField.GetAddress(convertedAppDomain, convertedThread);
        }

        /// <summary>
        ///     Gets the value of the field.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the field's value.</param>
        /// <param name="thread">The thread on which to get the field's value.</param>
        /// <returns>The value of the field.</returns>
        /// <inheritdoc />
        public object GetValue(IClrAppDomain appDomain, IClrThread thread)
        {
            var convertedAppDomain = (appDomain as ClrAppDomainAdapter)?.AppDomain;
            var convertedThread = (thread as ClrThreadAdapter)?.Thread;
            return ThreadStaticField.GetValue(convertedAppDomain, convertedThread);
        }

        /// <summary>
        ///     Gets the value of the field.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the field's value.</param>
        /// <param name="thread">The thread on which to get the field's value.</param>
        /// <param name="convertStrings">
        ///     When true, the value of a string field will be
        ///     returned as a System.String object; otherwise the address of the String object will be returned.
        /// </param>
        /// <returns>The value of the field.</returns>
        /// <inheritdoc />
        public object GetValue(IClrAppDomain appDomain, IClrThread thread, bool convertStrings)
        {
            var convertedAppDomain = (appDomain as ClrAppDomainAdapter)?.AppDomain;
            var convertedThread = (thread as ClrThreadAdapter)?.Thread;
            return ThreadStaticField.GetValue(convertedAppDomain, convertedThread, convertStrings);
        }

        public override void Setup()
        {
            ElementType = Converter.Convert(ThreadStaticField.ElementType);
            Type = Converter.Convert(ThreadStaticField.Type);
        }

        /// <summary>
        ///     Returns the element type of this field.  Note that even when Type is null, this should still tell you
        ///     the element type of the field.
        /// </summary>
        /// <value>The type of the element.</value>
        /// <inheritdoc />
        public ClrElementType ElementType { get; internal set; }

        /// <summary>
        ///     Returns true if this field has a simple value (meaning you may call "GetFieldValue" in one of the subtypes
        ///     of this class).
        /// </summary>
        /// <value><c>true</c> if this instance has simple value; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool HasSimpleValue { get; internal set; }

        /// <summary>
        ///     Returns true if this field is internal.
        /// </summary>
        /// <value><c>true</c> if this instance is internal; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsInternal { get; internal set; }

        /// <summary>
        ///     Returns true if this field is an object reference, false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is object reference; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsObjectReference { get; internal set; }

        /// <summary>
        ///     Returns true if this field is a primitive (int, float, etc), false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is primitive; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPrimitive { get; internal set; }

        /// <summary>
        ///     Returns true if this field is private.
        /// </summary>
        /// <value><c>true</c> if this instance is private; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPrivate { get; internal set; }

        /// <summary>
        ///     Returns true if this field is protected.
        /// </summary>
        /// <value><c>true</c> if this instance is protected; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsProtected { get; internal set; }

        /// <summary>
        ///     Returns true if this field is public.
        /// </summary>
        /// <value><c>true</c> if this instance is public; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPublic { get; internal set; }

        /// <summary>
        ///     Returns true if this field is a ValueClass (struct), false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is value class; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsValueClass { get; internal set; }

        /// <summary>
        ///     The name of the field.
        /// </summary>
        /// <value>The name.</value>
        /// <inheritdoc />
        public string Name { get; internal set; }

        /// <summary>
        ///     If the field has a well defined offset from the base of the object, return it (otherwise -1).
        /// </summary>
        /// <value>The offset.</value>
        /// <inheritdoc />
        public int Offset { get; internal set; }

        /// <summary>
        ///     Gets the size of this field.
        /// </summary>
        /// <value>The size.</value>
        /// <inheritdoc />
        public int Size { get; internal set; }

        /// <summary>
        ///     Returns the type token of this field.
        /// </summary>
        /// <value>The token.</value>
        /// <inheritdoc />
        public uint Token { get; internal set; }

        /// <summary>
        ///     The type of the field.  Note this property may return null on error.  There is a bug in several versions
        ///     of our debugging layer which causes this.  You should always null-check the return value of this field.
        /// </summary>
        /// <value>The type.</value>
        /// <inheritdoc />
        public IClrType Type { get; internal set; }
    }
}