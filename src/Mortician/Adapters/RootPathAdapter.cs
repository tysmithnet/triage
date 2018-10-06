// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="RootPathAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Linq;
using Microsoft.Diagnostics.Runtime;
using Mortician.Core.ClrMdAbstractions;

namespace Mortician.Adapters
{
    /// <summary>
    ///     Class RootPathAdapter.
    /// </summary>
    /// <seealso cref="Mortician.Core.ClrMdAbstractions.IRootPath" />
    internal class RootPathAdapter : BaseAdapter, IRootPath
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RootPathAdapter" /> class.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        /// <inheritdoc />
        public RootPathAdapter(IConverter converter, RootPath rootPath) : base(converter)
        {
            RootPath = rootPath;
        }

        /// <summary>
        ///     The root path
        /// </summary>
        internal RootPath RootPath;

        public override void Setup()
        {
            Path = RootPath.Path.Select(Converter.Convert).ToArray();
            Root = Converter.Convert(RootPath.Root);
        }

        /// <summary>
        ///     The path from Root to a given target object.
        /// </summary>
        /// <value>The path.</value>
        /// <inheritdoc />
        public IClrObject[] Path { get; internal set; }

        /// <summary>
        ///     The location that roots the object.
        /// </summary>
        /// <value>The root.</value>
        /// <inheritdoc />
        public IClrRoot Root { get; internal set; }
    }
}