namespace Triage.Mortician.Core.ClrMdAbstractions
{
    public interface IClrThreadStaticField
    {
        /// <summary>
        /// Gets the value of the field.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the field's value.</param>
        /// <param name="thread">The thread on which to get the field's value.</param>
        /// <returns>The value of the field.</returns>
        object GetValue(IClrAppDomain appDomain, IClrThread thread);

        /// <summary>
        /// Gets the value of the field.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the field's value.</param>
        /// <param name="thread">The thread on which to get the field's value.</param>
        /// <param name="convertStrings">When true, the value of a string field will be 
        /// returned as a System.String object; otherwise the address of the String object will be returned.</param>
        /// <returns>The value of the field.</returns>
        object GetValue(IClrAppDomain appDomain, IClrThread thread, bool convertStrings);

        /// <summary>
        /// Gets the address of the field.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the field's address.</param>
        /// <param name="thread">The thread on which to get the field's address.</param>
        /// <returns>The address of the field.</returns>
        ulong GetAddress(IClrAppDomain appDomain, IClrThread thread);

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
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        string ToString();
    }
}