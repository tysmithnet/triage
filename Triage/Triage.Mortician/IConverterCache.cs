// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-20-2018
// ***********************************************************************
// <copyright file="IConverterCache.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Triage.Mortician
{
    /// <summary>
    ///     Interface IConverterCache
    /// </summary>
    internal interface IConverterCache
    {
        /// <summary>
        ///     Determines whether [contains] [the specified instance].
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns><c>true</c> if [contains] [the specified instance]; otherwise, <c>false</c>.</returns>
        bool Contains(object instance);

        /// <summary>
        ///     Gets the or add.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="factoryMethod">The factory method.</param>
        /// <param name="setupFunction">The setup function.</param>
        /// <returns>T.</returns>
        T GetOrAdd<T>(object instance, Func<T> factoryMethod, Action setupFunction = null);
    }
}