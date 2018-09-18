namespace Triage.Mortician.Core
{
    /// <summary>
    /// The type of work item this is.
    /// </summary>
    public enum WorkItemKind
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// Callback for an async timer.
        /// </summary>
        AsyncTimer,

        /// <summary>
        /// Async callback.
        /// </summary>
        AsyncCallback,

        /// <summary>
        /// From ThreadPool.QueueUserWorkItem.
        /// </summary>
        QueueUserWorkItem,

        /// <summary>
        /// Timer delete callback.
        /// </summary>
        TimerDelete
    }

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