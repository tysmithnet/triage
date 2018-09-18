namespace Triage.Mortician.Core.ClrMdAbstractions
{
    public interface IHotColdRegions
    {
        /// <summary>
        /// Returns the start address of the method's hot region.
        /// </summary>
        ulong HotStart { get; }

        /// <summary>
        /// Returns the size of the hot region.
        /// </summary>
        uint HotSize { get; }

        /// <summary>
        /// Returns the start address of the method's cold region.
        /// </summary>
        ulong ColdStart { get; }

        /// <summary>
        /// Returns the size of the cold region.
        /// </summary>
        uint ColdSize { get; }
    }
}