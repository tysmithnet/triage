// ***********************************************************************
// Assembly         : Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IClrStaticField.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IClrStaticField
    /// </summary>
    public interface IClrStaticField : IClrField
    {
        /// <summary>
        ///     Returns the address of the static field's value in memory.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the field's address.</param>
        /// <returns>The address of the field's value.</returns>
        ulong GetAddress(IClrAppDomain appDomain);

        /// <summary>
        ///     The default value of the field.
        /// </summary>
        /// <returns>The default value of the field.</returns>
        object GetDefaultValue();

        /// <summary>
        ///     Gets the value of the static field.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the value.</param>
        /// <returns>The value of this static field.</returns>
        object GetValue(IClrAppDomain appDomain);

        /// <summary>
        ///     Gets the value of the static field.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the value.</param>
        /// <param name="convertStrings">
        ///     When true, the value of a string field will be
        ///     returned as a System.String object; otherwise the address of the String object will be returned.
        /// </param>
        /// <returns>The value of this static field.</returns>
        object GetValue(IClrAppDomain appDomain, bool convertStrings);

        /// <summary>
        ///     Returns whether this static field has been initialized in a particular AppDomain
        ///     or not.  If a static variable has not been initialized, then its class constructor
        ///     may have not been run yet.  Calling GetFieldValue on an uninitialized static
        ///     will result in returning either NULL or a value of 0.
        /// </summary>
        /// <param name="appDomain">The AppDomain to see if the variable has been initialized.</param>
        /// <returns>
        ///     True if the field has been initialized (even if initialized to NULL or a default
        ///     value), false if the runtime has not initialized this variable.
        /// </returns>
        bool IsInitialized(IClrAppDomain appDomain);

        /// <summary>
        ///     Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        string ToString();
    }
}