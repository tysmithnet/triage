namespace Triage.Mortician
{
    /// <summary>
    ///     Class DumpDomainModule. This class cannot be inherited.
    /// </summary>
    public sealed class DumpDomainModule
    {
        /// <summary>
        ///     Gets the address.
        /// </summary>
        /// <value>The address.</value>
        public ulong Address { get; internal set; }

        /// <summary>
        ///     Gets the location.
        /// </summary>
        /// <value>The location.</value>
        public string Location { get; internal set; }
    }
}