namespace Triage.Mortician
{
    /// <summary>
    ///     Class DumpDomainAppDomain. This class cannot be inherited.
    /// </summary>
    public sealed class DumpDomainAppDomain
    {
        /// <summary>
        ///     Gets the address.
        /// </summary>
        /// <value>The address.</value>
        public ulong Address { get; internal set; }

        /// <summary>
        ///     Gets the high frequency heap.
        /// </summary>
        /// <value>The high frequency heap.</value>
        public ulong HighFrequencyHeap { get; internal set; }

        /// <summary>
        ///     Gets the index.
        /// </summary>
        /// <value>The index.</value>
        public uint Index { get; internal set; }

        /// <summary>
        ///     Gets the low frequency heap.
        /// </summary>
        /// <value>The low frequency heap.</value>
        public ulong LowFrequencyHeap { get; internal set; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; internal set; }

        /// <summary>
        ///     Gets the stage.
        /// </summary>
        /// <value>The stage.</value>
        public AppDomainStage Stage { get; internal set; }

        /// <summary>
        ///     Gets the stub heap.
        /// </summary>
        /// <value>The stub heap.</value>
        public ulong StubHeap { get; internal set; }
    }
}