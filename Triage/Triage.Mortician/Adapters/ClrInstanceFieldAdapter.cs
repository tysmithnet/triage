// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-19-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ClrInstanceFieldAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrElementType = Triage.Mortician.Core.ClrMdAbstractions.ClrElementType;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class ClrInstanceFieldAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrInstanceField" />
    internal class ClrInstanceFieldAdapter : BaseAdapter, IClrInstanceField
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrInstanceFieldAdapter" /> class.
        /// </summary>
        /// <param name="instanceField">The instance field.</param>
        /// <exception cref="ArgumentNullException">instanceField</exception>
        /// <inheritdoc />
        public ClrInstanceFieldAdapter(IConverter converter, ClrInstanceField instanceField) : base(converter)
        {
            InstanceField = instanceField ?? throw new ArgumentNullException(nameof(instanceField));
        }

        /// <summary>
        ///     The instance field
        /// </summary>
        internal ClrInstanceField InstanceField;

        /// <summary>
        ///     Returns the address of the value of this field.  Equivalent to GetFieldAddress(objRef, false).
        /// </summary>
        /// <param name="objRef">The object to get the field address for.</param>
        /// <returns>The value of the field.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <inheritdoc />
        public ulong GetAddress(ulong objRef) => InstanceField.GetAddress(objRef);

        /// <summary>
        ///     Returns the address of the value of this field.  Equivalent to GetFieldAddress(objRef, false).
        /// </summary>
        /// <param name="objRef">The object to get the field address for.</param>
        /// <param name="interior">
        ///     Whether the enclosing type of this field is a value class,
        ///     and that value class is embedded in another object.
        /// </param>
        /// <returns>The value of the field.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <inheritdoc />
        public ulong GetAddress(ulong objRef, bool interior) => InstanceField.GetAddress(objRef, interior);

        /// <summary>
        ///     Returns the value of this field.  Equivalent to GetFieldValue(objRef, false).
        /// </summary>
        /// <param name="objRef">The object to get the field value for.</param>
        /// <returns>The value of the field.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <inheritdoc />
        public object GetValue(ulong objRef) => InstanceField.GetValue(objRef);

        /// <summary>
        ///     Returns the value of this field, optionally specifying if this field is
        ///     on a value class which is on the interior of another object.
        /// </summary>
        /// <param name="objRef">The object to get the field value for.</param>
        /// <param name="interior">
        ///     Whether the enclosing type of this field is a value class,
        ///     and that value class is embedded in another object.
        /// </param>
        /// <returns>The value of the field.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <inheritdoc />
        public object GetValue(ulong objRef, bool interior) => InstanceField.GetValue(objRef, interior);

        /// <summary>
        ///     Returns the value of this field, optionally specifying if this field is
        ///     on a value class which is on the interior of another object.
        /// </summary>
        /// <param name="objRef">The object to get the field value for.</param>
        /// <param name="interior">
        ///     Whether the enclosing type of this field is a value class,
        ///     and that value class is embedded in another object.
        /// </param>
        /// <param name="convertStrings">
        ///     When true, the value of a string field will be
        ///     returned as a System.String object; otherwise the address of the String object will be returned.
        /// </param>
        /// <returns>The value of the field.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <inheritdoc />
        public object GetValue(ulong objRef, bool interior, bool convertStrings) => InstanceField.GetValue(objRef);

        public override void Setup()
        {
            ElementType = Converter.Convert(InstanceField.ElementType);
            Type = Converter.Convert(InstanceField.Type);
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
        public bool HasSimpleValue => InstanceField.HasSimpleValue;

        /// <summary>
        ///     Returns true if this field is internal.
        /// </summary>
        /// <value><c>true</c> if this instance is internal; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsInternal => InstanceField.IsInternal;

        /// <summary>
        ///     Returns true if this field is an object reference, false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is object reference; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsObjectReference => InstanceField.IsObjectReference;

        /// <summary>
        ///     Returns true if this field is a primitive (int, float, etc), false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is primitive; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPrimitive => InstanceField.IsPrimitive;

        /// <summary>
        ///     Returns true if this field is private.
        /// </summary>
        /// <value><c>true</c> if this instance is private; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPrivate => InstanceField.IsPrivate;

        /// <summary>
        ///     Returns true if this field is protected.
        /// </summary>
        /// <value><c>true</c> if this instance is protected; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsProtected => InstanceField.IsProtected;

        /// <summary>
        ///     Returns true if this field is public.
        /// </summary>
        /// <value><c>true</c> if this instance is public; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPublic => InstanceField.IsPublic;

        /// <summary>
        ///     Returns true if this field is a ValueClass (struct), false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is value class; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsValueClass => InstanceField.IsValueClass;

        /// <summary>
        ///     The name of the field.
        /// </summary>
        /// <value>The name.</value>
        /// <inheritdoc />
        public string Name => InstanceField.Name;

        /// <summary>
        ///     If the field has a well defined offset from the base of the object, return it (otherwise -1).
        /// </summary>
        /// <value>The offset.</value>
        /// <inheritdoc />
        public int Offset => InstanceField.Offset;

        /// <summary>
        ///     Gets the size of this field.
        /// </summary>
        /// <value>The size.</value>
        /// <inheritdoc />
        public int Size => InstanceField.Size;

        /// <summary>
        ///     Returns the type token of this field.
        /// </summary>
        /// <value>The token.</value>
        /// <inheritdoc />
        public uint Token => InstanceField.Token;

        /// <summary>
        ///     The type of the field.  Note this property may return null on error.  There is a bug in several versions
        ///     of our debugging layer which causes this.  You should always null-check the return value of this field.
        /// </summary>
        /// <value>The type.</value>
        /// <inheritdoc />
        public IClrType Type { get; internal set; }
    }
}