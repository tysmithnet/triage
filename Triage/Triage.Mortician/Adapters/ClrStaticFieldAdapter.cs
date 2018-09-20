// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ClrStaticFieldAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrElementType = Triage.Mortician.Core.ClrMdAbstractions.ClrElementType;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class ClrStaticFieldAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrStaticField" />
    internal class ClrStaticFieldAdapter : BaseAdapter, IClrStaticField
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrStaticFieldAdapter" /> class.
        /// </summary>
        /// <param name="converter">Converter to use</param>
        /// <param name="staticField">The static field.</param>
        /// <exception cref="ArgumentNullException">staticField</exception>
        /// <inheritdoc />
        public ClrStaticFieldAdapter(IConverter converter, ClrStaticField staticField) : base(converter)
        {
            StaticField = staticField ?? throw new ArgumentNullException(nameof(staticField));
            ElementType = Converter.Convert(staticField.ElementType);
            Type = Converter.Convert(staticField.Type);
        }

        /// <summary>
        ///     The static field
        /// </summary>
        internal ClrStaticField StaticField;

        /// <summary>
        ///     Returns the address of the static field's value in memory.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the field's address.</param>
        /// <returns>The address of the field's value.</returns>
        /// <inheritdoc />
        public ulong GetAddress(IClrAppDomain appDomain) =>
            StaticField.GetAddress((appDomain as ClrAppDomainAdapter)?.AppDomain);

        /// <summary>
        ///     The default value of the field.
        /// </summary>
        /// <returns>The default value of the field.</returns>
        /// <inheritdoc />
        public object GetDefaultValue() => StaticField.GetDefaultValue();

        /// <summary>
        ///     Gets the value of the static field.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the value.</param>
        /// <returns>The value of this static field.</returns>
        /// <inheritdoc />
        public object GetValue(IClrAppDomain appDomain) =>
            StaticField.GetValue((appDomain as ClrAppDomainAdapter)?.AppDomain);

        /// <summary>
        ///     Gets the value of the static field.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the value.</param>
        /// <param name="convertStrings">
        ///     When true, the value of a string field will be
        ///     returned as a System.String object; otherwise the address of the String object will be returned.
        /// </param>
        /// <returns>The value of this static field.</returns>
        /// <inheritdoc />
        public object GetValue(IClrAppDomain appDomain, bool convertStrings) =>
            StaticField.GetValue((appDomain as ClrAppDomainAdapter)?.AppDomain, convertStrings);

        /// <summary>
        ///     Returns whether this static field has been initialized in a particular AppDomain
        ///     or not.  If a static variable has not been initialized, then its class constructor
        ///     may have not been run yet.  Calling GetFieldValue on an uninitialized static
        ///     will result in returning either NULL or a value of 0.
        /// </summary>
        /// <param name="appDomain">The AppDomain to see if the variable has been initialized.</param>
        /// <returns>
        ///     True if the field has been initialized (even if initialized to NULL or a default
        ///     value), false if the runtime has not initialized this variable.
        /// </returns>
        /// <inheritdoc />
        public bool IsInitialized(IClrAppDomain appDomain) =>
            StaticField.IsInitialized((appDomain as ClrAppDomainAdapter)?.AppDomain);

        /// <summary>
        ///     Returns the element type of this field.  Note that even when Type is null, this should still tell you
        ///     the element type of the field.
        /// </summary>
        /// <value>The type of the element.</value>
        /// <inheritdoc />
        public ClrElementType ElementType { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance has default value.
        /// </summary>
        /// <value><c>true</c> if this instance has default value; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool HasDefaultValue => StaticField.HasDefaultValue;

        /// <summary>
        ///     Returns true if this field has a simple value (meaning you may call "GetFieldValue" in one of the subtypes
        ///     of this class).
        /// </summary>
        /// <value><c>true</c> if this instance has simple value; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool HasSimpleValue => StaticField.HasSimpleValue;

        /// <summary>
        ///     Returns true if this field is internal.
        /// </summary>
        /// <value><c>true</c> if this instance is internal; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsInternal => StaticField.IsInternal;

        /// <summary>
        ///     Returns true if this field is an object reference, false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is object reference; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsObjectReference => StaticField.IsObjectReference;

        /// <summary>
        ///     Returns true if this field is a primitive (int, float, etc), false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is primitive; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPrimitive => StaticField.IsPrimitive;

        /// <summary>
        ///     Returns true if this field is private.
        /// </summary>
        /// <value><c>true</c> if this instance is private; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPrivate => StaticField.IsPrivate;

        /// <summary>
        ///     Returns true if this field is protected.
        /// </summary>
        /// <value><c>true</c> if this instance is protected; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsProtected => StaticField.IsProtected;

        /// <summary>
        ///     Returns true if this field is public.
        /// </summary>
        /// <value><c>true</c> if this instance is public; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPublic => StaticField.IsPublic;

        /// <summary>
        ///     Returns true if this field is a ValueClass (struct), false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is value class; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsValueClass => StaticField.IsValueClass;

        /// <summary>
        ///     The name of the field.
        /// </summary>
        /// <value>The name.</value>
        /// <inheritdoc />
        public string Name => StaticField.Name;

        /// <summary>
        ///     If the field has a well defined offset from the base of the object, return it (otherwise -1).
        /// </summary>
        /// <value>The offset.</value>
        /// <inheritdoc />
        public int Offset => StaticField.Offset;

        /// <summary>
        ///     Gets the size of this field.
        /// </summary>
        /// <value>The size.</value>
        /// <inheritdoc />
        public int Size => StaticField.Size;

        /// <summary>
        ///     Returns the type token of this field.
        /// </summary>
        /// <value>The token.</value>
        /// <inheritdoc />
        public uint Token => StaticField.Token;

        /// <summary>
        ///     The type of the field.  Note this property may return null on error.  There is a bug in several versions
        ///     of our debugging layer which causes this.  You should always null-check the return value of this field.
        /// </summary>
        /// <value>The type.</value>
        /// <inheritdoc />
        public IClrType Type { get; }
    }
}