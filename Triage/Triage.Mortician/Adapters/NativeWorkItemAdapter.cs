// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="NativeWorkItemAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;
using WorkItemKind = Triage.Mortician.Core.ClrMdAbstractions.WorkItemKind;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class NativeWorkItemAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.INativeWorkItem" />
    internal class NativeWorkItemAdapter : INativeWorkItem
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeWorkItemAdapter" /> class.
        /// </summary>
        /// <param name="nativeWorkItem">The native work item.</param>
        /// <exception cref="ArgumentNullException">nativeWorkItem</exception>
        /// <inheritdoc />
        public NativeWorkItemAdapter(NativeWorkItem nativeWorkItem)
        {
            NativeWorkItem = nativeWorkItem ?? throw new ArgumentNullException(nameof(nativeWorkItem));
            Kind = Converter.Convert(nativeWorkItem.Kind);
        }

        /// <summary>
        ///     The native work item
        /// </summary>
        internal NativeWorkItem NativeWorkItem;

        /// <summary>
        ///     Returns the callback's address.
        /// </summary>
        /// <value>The callback.</value>
        /// <inheritdoc />
        public ulong Callback => NativeWorkItem.Callback;

        /// <summary>
        ///     Returns the pointer to the user's data.
        /// </summary>
        /// <value>The data.</value>
        /// <inheritdoc />
        public ulong Data => NativeWorkItem.Data;

        /// <summary>
        ///     The type of work item this is.
        /// </summary>
        /// <value>The kind.</value>
        /// <inheritdoc />
        public WorkItemKind Kind { get; }

        /// <summary>
        ///     Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        [Import]
        internal IConverter Converter { get; set; }
    }
}