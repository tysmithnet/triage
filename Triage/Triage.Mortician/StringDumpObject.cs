// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-12-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="StringDumpObject.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Triage.Mortician.Core;
using Triage.Mortician.Domain;

namespace Triage.Mortician
{
    /// <summary>
    ///     Represents a System.String object from the managed heap
    /// </summary>
    /// <seealso cref="DumpObject" />
    /// <inheritdoc />
    /// <seealso cref="T:Triage.Mortician.DumpObject" />
    public class StringDumpObject : DumpObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Triage.Mortician.StringDumpObject" /> class.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="fullTypeName">Full name of the type.</param>
        /// <param name="size">The size.</param>
        /// <param name="value">The value.</param>
        /// <param name="gen">The gen.</param>
        /// <inheritdoc />
        public StringDumpObject(ulong address, string fullTypeName, ulong size, string value, int gen) : base(address,
            fullTypeName, size, gen)
        {
            Value = value;
        }

        /// <summary>
        ///     Get a short description of the object.
        /// </summary>
        /// <returns>A short description of this object</returns>
        /// <inheritdoc />
        /// <remarks>The return value is intended to be shown on a single line</remarks>
        protected override string ToShortDescription()
        {
            return base.ToShortDescription() + $" - {Value}";
        }

        /// <summary>
        ///     The string value from this heap object
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; internal set; }
    }
}