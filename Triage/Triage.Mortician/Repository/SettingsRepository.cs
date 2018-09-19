// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="SettingsRepository.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Triage.Mortician.Core;

namespace Triage.Mortician.Repository
{
    /// <summary>
    ///     Represents an object that is capable of getting the settings for this process
    /// </summary>
    /// <seealso cref="ISettingsRepository" />
    public class SettingsRepository : ISettingsRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SettingsRepository" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <exception cref="System.ArgumentNullException">settings</exception>
        /// <exception cref="ArgumentNullException">settings</exception>
        protected internal SettingsRepository(Dictionary<string, string> settings)
        {
            SettingsInternal = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        ///     Gets the setting associated with the provided key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ArgumentException">key</exception>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public string Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException($"{nameof(key)} cannot be null or whitespace");

            if (SettingsInternal.TryGetValue(key, out var value))
                return value;
            throw new ArgumentOutOfRangeException($"Could not find setting {key}");
        }

        /// <summary>
        ///     Gets the boolean value associated with the provided key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="fallbackToDefault">if set to <c>true</c> use default(T).</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool GetBool(string key, bool fallbackToDefault = false)
        {
            var s = Get(key);
            if (!fallbackToDefault) return Convert.ToBoolean(s);
            try
            {
                return Convert.ToBoolean(s);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Gets or sets the internal settings
        /// </summary>
        /// <value>The settings internal.</value>
        protected internal Dictionary<string, string> SettingsInternal { get; set; }
    }
}