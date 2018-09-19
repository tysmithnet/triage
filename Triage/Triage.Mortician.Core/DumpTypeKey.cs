// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 12-19-2017
// ***********************************************************************
// <copyright file="DumpTypeKey.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Type used to represent a unique type extracted from the memory dump
    ///     The method table alone is not sufficent to identify a type because generics
    ///     can share the same method table
    /// </summary>
    public struct DumpTypeKey
    {
        /// <summary>
        ///     Gets or sets the method table.
        /// </summary>
        /// <value>The method table.</value>
        public ulong MethodTable { get; set; }

        /// <summary>
        ///     Gets or sets the name of the type.
        /// </summary>
        /// <value>The name of the type.</value>
        public string TypeName { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpTypeKey" /> struct.
        /// </summary>
        /// <param name="methodTable">The method table.</param>
        /// <param name="typeName">Name of the type.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">methodTable</exception>
        /// <exception cref="System.ArgumentNullException">typeName</exception>
        public DumpTypeKey(ulong methodTable, string typeName)
        {
            if (methodTable == 0)
                throw new ArgumentOutOfRangeException(
                    $"{nameof(methodTable)} cannot be 0. No methods tables are loaded at 0x0");

            MethodTable = methodTable;
            TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
        }
    }
}