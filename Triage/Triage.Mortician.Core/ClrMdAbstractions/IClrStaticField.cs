namespace Triage.Mortician.Core.ClrMdAbstractions
{
    public interface IClrStaticField
    {
        /// <summary>
        /// Returns whether this static field has been initialized in a particular AppDomain
        /// or not.  If a static variable has not been initialized, then its class constructor
        /// may have not been run yet.  Calling GetFieldValue on an uninitialized static
        /// will result in returning either NULL or a value of 0.
        /// </summary>
        /// <param name="appDomain">The AppDomain to see if the variable has been initialized.</param>
        /// <returns>True if the field has been initialized (even if initialized to NULL or a default
        /// value), false if the runtime has not initialized this variable.</returns>
        bool IsInitialized(IClrAppDomain appDomain);

        /// <summary>
        /// Gets the value of the static field.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the value.</param>
        /// <returns>The value of this static field.</returns>
        object GetValue(IClrAppDomain appDomain);

        /// <summary>
        /// Gets the value of the static field.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the value.</param>
        /// <param name="convertStrings">When true, the value of a string field will be 
        /// returned as a System.String object; otherwise the address of the String object will be returned.</param>
        /// <returns>The value of this static field.</returns>
        object GetValue(IClrAppDomain appDomain, bool convertStrings);

        /// <summary>
        /// Returns the address of the static field's value in memory.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the field's address.</param>
        /// <returns>The address of the field's value.</returns>
        ulong GetAddress(IClrAppDomain appDomain);

        /// <summary>
        /// Returns true if the static field has a default value (and if we can obtain it).
        /// </summary>
        bool HasDefaultValue { get; }

        /// <summary>
        /// The name of the field.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns the type token of this field.
        /// </summary>
        uint Token { get; }

        /// <summary>
        /// The type of the field.  Note this property may return null on error.  There is a bug in several versions
        /// of our debugging layer which causes this.  You should always null-check the return value of this field.
        /// </summary>
        IClrType Type { get; }

        /// <summary>
        /// Returns the element type of this field.  Note that even when Type is null, this should still tell you
        /// the element type of the field.
        /// </summary>
        ClrElementType ElementType { get; }

        /// <summary>
        /// Returns true if this field is a primitive (int, float, etc), false otherwise.
        /// </summary>
        /// <returns>True if this field is a primitive (int, float, etc), false otherwise.</returns>
        bool IsPrimitive { get; }

        /// <summary>
        /// Returns true if this field is a ValueClass (struct), false otherwise.
        /// </summary>
        /// <returns>True if this field is a ValueClass (struct), false otherwise.</returns>
        bool IsValueClass { get; }

        /// <summary>
        /// Returns true if this field is an object reference, false otherwise.
        /// </summary>
        /// <returns>True if this field is an object reference, false otherwise.</returns>
        bool IsObjectReference { get; }

        /// <summary>
        /// Gets the size of this field.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Returns true if this field is public.
        /// </summary>
        bool IsPublic { get; }

        /// <summary>
        /// Returns true if this field is private.
        /// </summary>
        bool IsPrivate { get; }

        /// <summary>
        /// Returns true if this field is internal.
        /// </summary>
        bool IsInternal { get; }

        /// <summary>
        /// Returns true if this field is protected.
        /// </summary>
        bool IsProtected { get; }

        /// <summary>
        /// Returns true if this field has a simple value (meaning you may call "GetFieldValue" in one of the subtypes
        /// of this class).
        /// </summary>
        bool HasSimpleValue { get; }

        /// <summary>
        /// If the field has a well defined offset from the base of the object, return it (otherwise -1). 
        /// </summary>
        int Offset { get; }

        /// <summary>
        /// The default value of the field.
        /// </summary>
        /// <returns>The default value of the field.</returns>
        object GetDefaultValue();

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        string ToString();
    }
}