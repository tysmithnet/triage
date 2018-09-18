namespace Triage.Mortician.Core
{
    public interface IComInterfaceData
    {
        /// <summary>
        /// The CLR type this represents.
        /// </summary>
        IClrType Type { get; }

        /// <summary>
        /// The interface pointer of Type.
        /// </summary>
        ulong InterfacePointer { get; }
    }
}