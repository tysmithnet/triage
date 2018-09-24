// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="PdbInfoAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class PdbInfoAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IPdbInfo" />
    internal class PdbInfoAdapter : BaseAdapter, IPdbInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PdbInfoAdapter" /> class.
        /// </summary>
        /// <param name="pdbInfo">The PDB information.</param>
        /// <exception cref="ArgumentNullException">pdbInfo</exception>
        /// <inheritdoc />
        public PdbInfoAdapter(IConverter converter, PdbInfo pdbInfo) : base(converter)
        {
            PdbInfo = pdbInfo ?? throw new ArgumentNullException(nameof(pdbInfo));
        }

        /// <summary>
        ///     The PDB information
        /// </summary>
        internal PdbInfo PdbInfo;

        public override void Setup()
        {
        }

        /// <summary>
        ///     The filename of the pdb.
        /// </summary>
        /// <value>The name of the file.</value>
        /// <inheritdoc />
        public string FileName => PdbInfo.FileName;

        /// <summary>
        ///     The Guid of the PDB.
        /// </summary>
        /// <value>The unique identifier.</value>
        /// <inheritdoc />
        public Guid Guid => PdbInfo.Guid;

        /// <summary>
        ///     The pdb revision.
        /// </summary>
        /// <value>The revision.</value>
        /// <inheritdoc />
        public int Revision => PdbInfo.Revision;
    }
}