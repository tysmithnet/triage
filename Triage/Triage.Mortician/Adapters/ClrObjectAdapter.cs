// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ClrObjectAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class ClrObjectAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrObject" />
    internal class ClrObjectAdapter : BaseAdapter, IClrObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrObjectAdapter" /> class.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <inheritdoc />
        public ClrObjectAdapter(IConverter converter, ClrObject o) : base(converter)
        {
            Object = o;
            Type = Converter.Convert(o.Type);
        }

        /// <summary>
        ///     The object
        /// </summary>
        internal ClrObject Object;

        /// <summary>
        ///     Enumerates all objects that this object references.
        /// </summary>
        /// <param name="carefully">if set to <c>true</c> [carefully].</param>
        /// <returns>An enumeration of object references.</returns>
        /// <inheritdoc />
        public IEnumerable<IClrObject> EnumerateObjectReferences(bool carefully = false) =>
            Object.EnumerateObjectReferences(carefully).Select(Converter.Convert);

        /// <summary>
        ///     Determines if this instance and another specific <see cref="!:ClrObject" /> have the same value.
        ///     <para>Instances are considered equal when they have same <see cref="!:ClrObject.Address" />.</para>
        /// </summary>
        /// <param name="other">The <see cref="!:ClrObject" /> to compare to this instance.</param>
        /// <returns>
        ///     <c>true</c> if the <see cref="!:ClrObject.Address" /> of the parameter is same as
        ///     <see cref="!:ClrObject.Address" /> in this instance; <c>false</c> otherwise.
        /// </returns>
        /// <inheritdoc />
        public bool Equals(IClrObject other) => Object.Equals((other as ClrObjectAdapter)?.Object);

        /// <summary>
        ///     Gets the value of a primitive field.  This will throw an InvalidCastException if the type parameter
        ///     does not match the field's type.
        /// </summary>
        /// <typeparam name="T">The type of the field itself.</typeparam>
        /// <param name="fieldName">The name of the field.</param>
        /// <returns>The value of this field.</returns>
        /// <inheritdoc />
        public T GetField<T>(string fieldName) where T : struct => Object.GetField<T>(fieldName);

        /// <summary>
        ///     Gets the given object reference field from this ClrObject.  Throws ArgumentException if the given field does
        ///     not exist in the object.  Throws NullReferenceException if IsNull is true.
        /// </summary>
        /// <param name="fieldName">The name of the field to retrieve.</param>
        /// <returns>A ClrObject of the given field.</returns>
        /// <inheritdoc />
        public IClrObject GetObjectField(string fieldName) => Converter.Convert(Object.GetObjectField(fieldName));

        /// <summary>
        ///     Gets a string field from the object.  Note that the type must match exactly, as this method
        ///     will not do type coercion.  This method will throw an ArgumentException if no field matches
        ///     the given name.  It will throw a NullReferenceException if the target object is null (that is,
        ///     if (IsNull returns true).  It will throw an InvalidOperationException if the field is not
        ///     of the correct type.  Lastly, it will throw a MemoryReadException if there was an error reading
        ///     the value of this field out of the data target.
        /// </summary>
        /// <param name="fieldName">The name of the field to get the value for.</param>
        /// <returns>The value of the given field.</returns>
        /// <inheritdoc />
        public string GetStringField(string fieldName) => Object.GetStringField(fieldName);

        /// <summary>
        ///     Gets the value class field.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>IClrValueClass.</returns>
        /// <inheritdoc />
        public IClrValueClass GetValueClassField(string fieldName) =>
            Converter.Convert(Object.GetValueClassField(fieldName));

        /// <summary>
        ///     The address of the object.
        /// </summary>
        /// <value>The address.</value>
        /// <inheritdoc />
        public ulong Address => Object.Address;

        /// <summary>
        ///     Returns true if this object possibly contians GC pointers.
        /// </summary>
        /// <value><c>true</c> if [contains pointers]; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool ContainsPointers => Object.ContainsPointers;

        /// <summary>
        ///     The address of the object in Hex format.
        /// </summary>
        /// <value>The hexadecimal address.</value>
        /// <inheritdoc />
        public string HexAddress => Object.HexAddress;

        /// <summary>
        ///     Returns whether this object is an array or not.
        /// </summary>
        /// <value><c>true</c> if this instance is array; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsArray => Object.IsArray;

        /// <summary>
        ///     Returns whether this object is actually a boxed primitive or struct.
        /// </summary>
        /// <value><c>true</c> if this instance is boxed; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsBoxed => Object.IsBoxed;

        /// <summary>
        ///     Returns if the object value is null.
        /// </summary>
        /// <value><c>true</c> if this instance is null; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsNull => Object.IsNull;

        /// <summary>
        ///     Returns the count of elements in this array, or throws InvalidOperatonException if this object is not an array.
        /// </summary>
        /// <value>The length.</value>
        /// <inheritdoc />
        public int Length => Object.Length;

        /// <summary>
        ///     Gets the size of the object.
        /// </summary>
        /// <value>The size.</value>
        /// <inheritdoc />
        public ulong Size => Object.Size;

        /// <summary>
        ///     The type of the object.
        /// </summary>
        /// <value>The type.</value>
        /// <inheritdoc />
        public IClrType Type { get; }

        /// <summary>
        ///     Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        [Import]
        internal IConverter Converter { get; set; }
    }
}