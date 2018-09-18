namespace Microsoft.Diagnostics.Runtime
{
    public interface IComInterfaceData
    {
        /// <summary>
        /// The CLR type this represents.
        /// </summary>
        ClrType Type { get; }

        /// <summary>
        /// The interface pointer of Type.
        /// </summary>
        ulong InterfacePointer { get; }
    }
}