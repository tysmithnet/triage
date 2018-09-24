// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-20-2018
// ***********************************************************************
// <copyright file="BaseAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class BaseAdapter.
    /// </summary>
    internal abstract class BaseAdapter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseAdapter" /> class.
        /// </summary>
        /// <param name="converter">The converter.</param>
        /// <exception cref="ArgumentNullException">converter</exception>
        /// <inheritdoc />
        protected BaseAdapter(IConverter converter)
        {
            Converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        ///     Setups this instance.
        /// </summary>
        public abstract void Setup();

        /// <summary>
        ///     Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        internal IConverter Converter { get; set; }
    }
}