// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ClrValueClassAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel.Composition;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class ClrValueClassAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrValueClass" />
    internal class ClrValueClassAdapter : BaseAdapter, IClrValueClass
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrValueClassAdapter" /> class.
        /// </summary>
        /// <param name="valueClass">The value class.</param>
        /// <inheritdoc />
        public ClrValueClassAdapter(IConverter converter, ClrValueClass valueClass) : base(converter)
        {
            ValueClass = valueClass;
            Type = Converter.Convert(valueClass.Type);
        }

        /// <summary>
        ///     The value class
        /// </summary>
        internal ClrValueClass ValueClass;

        /// <summary>
        ///     Gets the value of a primitive field.  This will throw an InvalidCastException if the type parameter
        ///     does not match the field's type.
        /// </summary>
        /// <typeparam name="T">The type of the field itself.</typeparam>
        /// <param name="fieldName">The name of the field.</param>
        /// <returns>The value of this field.</returns>
        /// <inheritdoc />
        public T GetField<T>(string fieldName) where T : struct => ValueClass.GetField<T>(fieldName);

        /// <summary>
        ///     Gets the given object reference field from this ClrObject.  Throws ArgumentException if the given field does
        ///     not exist in the object.  Throws NullReferenceException if IsNull is true.
        /// </summary>
        /// <param name="fieldName">The name of the field to retrieve.</param>
        /// <returns>A ClrObject of the given field.</returns>
        /// <inheritdoc />
        public IClrObject GetObjectField(string fieldName) => Converter.Convert(ValueClass.GetObjectField(fieldName));

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
        public string GetStringField(string fieldName) => ValueClass.GetStringField(fieldName);

        /// <summary>
        ///     Gets the value class field.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>IClrValueClass.</returns>
        /// <inheritdoc />
        public IClrValueClass GetValueClassField(string fieldName) =>
            Converter.Convert(ValueClass.GetValueClassField(fieldName));

        /// <summary>
        ///     The address of the object.
        /// </summary>
        /// <value>The address.</value>
        /// <inheritdoc />
        public ulong Address => ValueClass.Address;

        /// <summary>
        ///     The address of the object in Hex format.
        /// </summary>
        /// <value>The hexadecimal address.</value>
        /// <inheritdoc />
        public string HexAddress => ValueClass.HexAddress;

        /// <summary>
        ///     The type of the object.
        /// </summary>
        /// <value>The type.</value>
        /// <inheritdoc />
        public IClrType Type { get; }
        
    }
}