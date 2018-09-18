namespace Triage.Mortician.Core.ClrMdAbstractions
{
    public interface IILInfo
    {
        /// <summary>
        /// The address in memory of where the IL for a particular method is located.
        /// </summary>
        ulong Address { get; }

        /// <summary>
        /// The length (in bytes) of the IL method body.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// The maximum IL stack size in this method.
        /// </summary>
        int MaxStack { get; }

        /// <summary>
        /// The flags associated with the IL code.
        /// </summary>
        uint Flags { get; }

        /// <summary>
        /// The local variable signature token for this IL method.
        /// </summary>
        uint LocalVarSignatureToken { get; }
    }
}