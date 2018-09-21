namespace Triage.Mortician
{
    /// <summary>
    ///     Class AppDomainStage.
    /// </summary>
    public abstract class AppDomainStage
    {
        /// <summary>
        ///     The open
        /// </summary>
        public static readonly OpenAppDomainStage Open = new OpenAppDomainStage();

        /// <summary>
        ///     Gets the display text.
        /// </summary>
        /// <value>The display text.</value>
        public abstract string DisplayText { get; internal set; }

        /// <summary>
        ///     Class OpenAppDomainStage.
        /// </summary>
        /// <seealso cref="Triage.Mortician.AppDomainStage" />
        public class OpenAppDomainStage : AppDomainStage
        {
            /// <summary>
            ///     Gets the display text.
            /// </summary>
            /// <value>The display text.</value>
            /// <inheritdoc />
            public override string DisplayText { get; internal set; } = "Open";
        }
    }
}