// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 10-01-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="DumpTypeField.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Class DumpTypeField.
    /// </summary>
    public class DumpTypeField
    {
        /// <summary>
        ///     Gets or sets the type of the element.
        /// </summary>
        /// <value>The type of the element.</value>
        public ClrElementType ElementType { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance has simple value.
        /// </summary>
        /// <value><c>true</c> if this instance has simple value; otherwise, <c>false</c>.</value>
        public bool HasSimpleValue { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is internal.
        /// </summary>
        /// <value><c>true</c> if this instance is internal; otherwise, <c>false</c>.</value>
        public bool IsInternal { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is object reference.
        /// </summary>
        /// <value><c>true</c> if this instance is object reference; otherwise, <c>false</c>.</value>
        public bool IsObjectReference { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is primitive.
        /// </summary>
        /// <value><c>true</c> if this instance is primitive; otherwise, <c>false</c>.</value>
        public bool IsPrimitive { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is private.
        /// </summary>
        /// <value><c>true</c> if this instance is private; otherwise, <c>false</c>.</value>
        public bool IsPrivate { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is protected.
        /// </summary>
        /// <value><c>true</c> if this instance is protected; otherwise, <c>false</c>.</value>
        public bool IsProtected { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is public.
        /// </summary>
        /// <value><c>true</c> if this instance is public; otherwise, <c>false</c>.</value>
        public bool IsPublic { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is value class.
        /// </summary>
        /// <value><c>true</c> if this instance is value class; otherwise, <c>false</c>.</value>
        public bool IsValueClass { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the offset.
        /// </summary>
        /// <value>The offset.</value>
        public int Offset { get; set; }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public int Size { get; set; }

        /// <summary>
        ///     Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        public uint Token { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public DumpType Type { get; set; }
    }
}