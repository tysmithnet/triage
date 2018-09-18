// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="ISettingsRepository.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician.Repository
{
    /// <summary>
    ///     Interface ISettingsRepository
    /// </summary>
    public interface ISettingsRepository
    {
        /// <summary>
        ///     Gets the setting associated with the provided key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        string Get(string key);

        /// <summary>
        ///     Gets the boolean value associated with the provided key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="fallbackToDefault">if set to <c>true</c> use default(T).</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool GetBool(string key, bool fallbackToDefault = false);
    }
}