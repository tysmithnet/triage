// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IClrObject.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IClrObject
    /// </summary>
    public interface IClrObject
    {
        /// <summary>
        ///     Enumerates all objects that this object references.
        /// </summary>
        /// <param name="carefully">if set to <c>true</c> [carefully].</param>
        /// <returns>An enumeration of object references.</returns>
        IEnumerable<IClrObject> EnumerateObjectReferences(bool carefully = false);

        /// <summary>
        ///     Determines if this instance and another specific <see cref="ClrObject" /> have the same value.
        ///     <para>Instances are considered equal when they have same <see cref="ClrObject.Address" />.</para>
        /// </summary>
        /// <param name="other">The <see cref="ClrObject" /> to compare to this instance.</param>
        /// <returns>
        ///     <c>true</c> if the <see cref="ClrObject.Address" /> of the parameter is same as
        ///     <see cref="ClrObject.Address" /> in this instance; <c>false</c> otherwise.
        /// </returns>
        bool Equals(IClrObject other);

        /// <summary>
        ///     Determines whether this instance and a specified object, which must also be a <see cref="ClrObject" />, have the
        ///     same value.
        /// </summary>
        /// <param name="other">The <see cref="ClrObject" /> to compare to this instance.</param>
        /// <returns>
        ///     <c>true</c> if <paramref name="other" /> is <see cref="ClrObject" />, and its <see cref="ClrObject.Address" />
        ///     is same as <see cref="ClrObject.Address" /> in this instance; <c>false</c> otherwise.
        /// </returns>
        bool Equals(object other);

        /// <summary>
        ///     Gets the value of a primitive field.  This will throw an InvalidCastException if the type parameter
        ///     does not match the field's type.
        /// </summary>
        /// <typeparam name="T">The type of the field itself.</typeparam>
        /// <param name="fieldName">The name of the field.</param>
        /// <returns>The value of this field.</returns>
        T GetField<T>(string fieldName) where T : struct;

        /// <summary>
        ///     Returns the hash code for this <see cref="ClrObject" /> based on its <see cref="ClrObject.Address" />.
        /// </summary>
        /// <returns>An <see cref="int" /> hash code for this instance.</returns>
        int GetHashCode();

        /// <summary>
        ///     Gets the given object reference field from this ClrObject.  Throws ArgumentException if the given field does
        ///     not exist in the object.  Throws NullReferenceException if IsNull is true.
        /// </summary>
        /// <param name="fieldName">The name of the field to retrieve.</param>
        /// <returns>A ClrObject of the given field.</returns>
        IClrObject GetObjectField(string fieldName);

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
        string GetStringField(string fieldName);

        /// <summary>
        ///     Gets the value class field.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>IClrValueClass.</returns>
        IClrValueClass GetValueClassField(string fieldName);

        /// <summary>
        ///     ToString override.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        string ToString();

        /// <summary>
        ///     The address of the object.
        /// </summary>
        /// <value>The address.</value>
        ulong Address { get; }

        /// <summary>
        ///     Returns true if this object possibly contians GC pointers.
        /// </summary>
        /// <value><c>true</c> if [contains pointers]; otherwise, <c>false</c>.</value>
        bool ContainsPointers { get; }

        /// <summary>
        ///     The address of the object in Hex format.
        /// </summary>
        /// <value>The hexadecimal address.</value>
        string HexAddress { get; }

        /// <summary>
        ///     Returns whether this object is an array or not.
        /// </summary>
        /// <value><c>true</c> if this instance is array; otherwise, <c>false</c>.</value>
        bool IsArray { get; }

        /// <summary>
        ///     Returns whether this object is actually a boxed primitive or struct.
        /// </summary>
        /// <value><c>true</c> if this instance is boxed; otherwise, <c>false</c>.</value>
        bool IsBoxed { get; }

        /// <summary>
        ///     Returns if the object value is null.
        /// </summary>
        /// <value><c>true</c> if this instance is null; otherwise, <c>false</c>.</value>
        bool IsNull { get; }

        /// <summary>
        ///     Returns the count of elements in this array, or throws InvalidOperatonException if this object is not an array.
        /// </summary>
        /// <value>The length.</value>
        int Length { get; }

        /// <summary>
        ///     Gets the size of the object.
        /// </summary>
        /// <value>The size.</value>
        ulong Size { get; }

        /// <summary>
        ///     The type of the object.
        /// </summary>
        /// <value>The type.</value>
        IClrType Type { get; }
    }
}