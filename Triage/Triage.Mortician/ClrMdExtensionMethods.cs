using Triage.Mortician.Core;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician
{
    /// <summary>
    ///     Class ClrMdExtensionMethods.
    /// </summary>
    internal static class ClrMdExtensionMethods
    {
        /// <summary>
        ///     To the type of the key.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <returns>DumpModuleKey.</returns>
        public static DumpModuleKey ToKeyType(this IClrModule module) =>
            new DumpModuleKey(module.AssemblyId, module.Name);

        /// <summary>
        ///     To the type of the key.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>DumpTypeKey.</returns>
        public static DumpTypeKey ToKeyType(this IClrType type) =>
            new DumpTypeKey(type.Module?.AssemblyId ?? 0, type.Name);
    }
}