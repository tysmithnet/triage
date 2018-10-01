// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-24-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-24-2018
// ***********************************************************************
// <copyright file="CodeLocation.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Class CodeLocation.
    /// </summary>
    public class CodeLocation
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CodeLocation" /> class.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="method">The method.</param>
        /// <param name="offset">The offset.</param>
        /// <inheritdoc />
        public CodeLocation(string module, string method, ulong offset)
        {
            Module = module;
            Method = method;
            Offset = offset;
        }

        /// <summary>
        ///     Gets the method.
        /// </summary>
        /// <value>The method.</value>
        public string Method { get; }

        /// <summary>
        ///     Gets the module.
        /// </summary>
        /// <value>The module.</value>
        public string Module { get; }

        /// <summary>
        ///     Gets the offset.
        /// </summary>
        /// <value>The offset.</value>
        public ulong Offset { get; }
    }
}