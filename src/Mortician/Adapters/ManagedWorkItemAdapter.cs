// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ManagedWorkItemAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Microsoft.Diagnostics.Runtime;
using Mortician.Core.ClrMdAbstractions;

namespace Mortician.Adapters
{
    /// <summary>
    ///     Class ManagedWorkItemAdapter.
    /// </summary>
    /// <seealso cref="Mortician.Core.ClrMdAbstractions.IManagedWorkItem" />
    internal class ManagedWorkItemAdapter : BaseAdapter, IManagedWorkItem
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ManagedWorkItemAdapter" /> class.
        /// </summary>
        /// <param name="workItem">The work item.</param>
        /// <exception cref="ArgumentNullException">workItem</exception>
        /// <inheritdoc />
        public ManagedWorkItemAdapter(IConverter converter, ManagedWorkItem workItem) : base(converter)
        {
            WorkItem = workItem ?? throw new ArgumentNullException(nameof(workItem));
            Object = WorkItem.Object;
        }

        /// <summary>
        ///     The work item
        /// </summary>
        internal ManagedWorkItem WorkItem;

        public override void Setup()
        {
            Type = Converter.Convert(WorkItem.Type);
        }

        /// <summary>
        ///     The object address of this entry.
        /// </summary>
        /// <value>The object.</value>
        /// <inheritdoc />
        public ulong Object { get; internal set; }

        /// <summary>
        ///     The type of Object.
        /// </summary>
        /// <value>The type.</value>
        /// <inheritdoc />
        public IClrType Type { get; internal set; }
    }
}