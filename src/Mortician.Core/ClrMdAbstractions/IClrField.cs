// ***********************************************************************
// Assembly         : Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IClrField.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IClrField
    /// </summary>
    public interface IClrField
    {
        /// <summary>
        ///     Returns the element type of this field.  Note that even when Type is null, this should still tell you
        ///     the element type of the field.
        /// </summary>
        /// <value>The type of the element.</value>
        ClrElementType ElementType { get; }

        /// <summary>
        ///     Returns true if this field has a simple value (meaning you may call "GetFieldValue" in one of the subtypes
        ///     of this class).
        /// </summary>
        /// <value><c>true</c> if this instance has simple value; otherwise, <c>false</c>.</value>
        bool HasSimpleValue { get; }

        /// <summary>
        ///     Returns true if this field is internal.
        /// </summary>
        /// <value><c>true</c> if this instance is internal; otherwise, <c>false</c>.</value>
        bool IsInternal { get; }

        /// <summary>
        ///     Returns true if this field is an object reference, false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is object reference; otherwise, <c>false</c>.</value>
        bool IsObjectReference { get; }

        /// <summary>
        ///     Returns true if this field is a primitive (int, float, etc), false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is primitive; otherwise, <c>false</c>.</value>
        bool IsPrimitive { get; }

        /// <summary>
        ///     Returns true if this field is private.
        /// </summary>
        /// <value><c>true</c> if this instance is private; otherwise, <c>false</c>.</value>
        bool IsPrivate { get; }

        /// <summary>
        ///     Returns true if this field is protected.
        /// </summary>
        /// <value><c>true</c> if this instance is protected; otherwise, <c>false</c>.</value>
        bool IsProtected { get; }

        /// <summary>
        ///     Returns true if this field is public.
        /// </summary>
        /// <value><c>true</c> if this instance is public; otherwise, <c>false</c>.</value>
        bool IsPublic { get; }

        /// <summary>
        ///     Returns true if this field is a ValueClass (struct), false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is value class; otherwise, <c>false</c>.</value>
        bool IsValueClass { get; }

        /// <summary>
        ///     The name of the field.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        ///     If the field has a well defined offset from the base of the object, return it (otherwise -1).
        /// </summary>
        /// <value>The offset.</value>
        int Offset { get; }

        /// <summary>
        ///     Gets the size of this field.
        /// </summary>
        /// <value>The size.</value>
        int Size { get; }

        /// <summary>
        ///     Returns the type token of this field.
        /// </summary>
        /// <value>The token.</value>
        uint Token { get; }

        /// <summary>
        ///     The type of the field.  Note this property may return null on error.  There is a bug in several versions
        ///     of our debugging layer which causes this.  You should always null-check the return value of this field.
        /// </summary>
        /// <value>The type.</value>
        IClrType Type { get; }
    }
}