namespace Triage.Mortician.Core.ClrMdAbstractions
{
    public interface INativeWorkItem
    {
        /// <summary>
        /// The type of work item this is.
        /// </summary>
        WorkItemKind Kind { get; }

        /// <summary>
        /// Returns the callback's address.
        /// </summary>
        ulong Callback { get; }

        /// <summary>
        /// Returns the pointer to the user's data.
        /// </summary>
        ulong Data { get; }
    }
}