﻿// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="VirtualQueryData.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     The result of a VirtualQuery.
    /// </summary>
    [Serializable]
    public struct VirtualQueryData
    {
        /// <summary>
        ///     The base address of the allocation.
        /// </summary>
        public ulong BaseAddress;

        /// <summary>
        ///     The size of the allocation.
        /// </summary>
        public ulong Size;

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="addr">Base address of the memory range.</param>
        /// <param name="size">The size of the memory range.</param>
        public VirtualQueryData(ulong addr, ulong size)
        {
            BaseAddress = addr;
            Size = size;
        }
    }
}