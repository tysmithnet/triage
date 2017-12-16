﻿namespace Triage.Mortician
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a System.String object from the managed heap
    /// </summary>
    /// <seealso cref="T:Triage.Mortician.DumpObject" />
    public class StringDumpObject : DumpObject
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Triage.Mortician.StringDumpObject" /> class.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="fullTypeName">Full name of the type.</param>
        /// <param name="size">The size.</param>
        /// <param name="value">The value.</param>
        /// <param name="gen">The gen.</param>
        public StringDumpObject(ulong address, string fullTypeName, ulong size, string value, int gen) : base(address,
            fullTypeName, size, gen)
        {
            Value = value;
        }

        /// <summary>
        ///     The string value from this heap object
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public string Value { get; internal set; }
    }
}