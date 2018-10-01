// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-24-2018
// ***********************************************************************
// <copyright file="AppDomainStage.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician
{
    /// <summary>
    ///     Class AppDomainStage.
    /// </summary>
    public abstract class AppDomainStage
    {
        /// <summary>
        ///     The active
        /// </summary>
        public static readonly ActiveAppDomainStage Active = new ActiveAppDomainStage();

        /// <summary>
        ///     The cleared
        /// </summary>
        public static readonly ClearedAppDomainStage Cleared = new ClearedAppDomainStage();

        /// <summary>
        ///     The closed
        /// </summary>
        public static readonly ClosedAppDomainStage Closed = new ClosedAppDomainStage();

        /// <summary>
        ///     The collected
        /// </summary>
        public static readonly CollectedAppDomainStage Collected = new CollectedAppDomainStage();

        /// <summary>
        ///     The creating
        /// </summary>
        public static readonly CreatingAppDomainStage Creating = new CreatingAppDomainStage();

        /// <summary>
        ///     The exited
        /// </summary>
        public static readonly ExitedAppDomainStage Exited = new ExitedAppDomainStage();

        /// <summary>
        ///     The exiting
        /// </summary>
        public static readonly ExitingAppDomainStage Exiting = new ExitingAppDomainStage();

        /// <summary>
        ///     The finalized
        /// </summary>
        public static readonly FinalizedAppDomainStage Finalized = new FinalizedAppDomainStage();

        /// <summary>
        ///     The finalizing
        /// </summary>
        public static readonly FinalizingAppDomainStage Finalizing = new FinalizingAppDomainStage();

        /// <summary>
        ///     The handle table no access
        /// </summary>
        public static readonly HandleTableNoAccessAppDomainStage HandleTableNoAccess =
            new HandleTableNoAccessAppDomainStage();

        /// <summary>
        ///     The open
        /// </summary>
        public static readonly OpenAppDomainStage Open = new OpenAppDomainStage();

        /// <summary>
        ///     The ready for managed code
        /// </summary>
        public static readonly ReadyForManagedCodeAppDomainStage ReadyForManagedCode =
            new ReadyForManagedCodeAppDomainStage();

        /// <summary>
        ///     The unknown
        /// </summary>
        public static readonly UnknownAppDomainStage Unknown = new UnknownAppDomainStage();

        /// <summary>
        ///     The unload requested
        /// </summary>
        public static readonly UnloadRequestedAppDomainStage UnloadRequested = new UnloadRequestedAppDomainStage();

        /// <summary>
        ///     All
        /// </summary>
        public static readonly AppDomainStage[] All =
        {
            Creating,
            ReadyForManagedCode,
            Active,
            Open,
            UnloadRequested,
            Exiting,
            Exited,
            Finalizing,
            Finalized,
            HandleTableNoAccess,
            Cleared,
            Collected,
            Closed,
            Unknown
        };

        /// <summary>
        ///     Gets the display text.
        /// </summary>
        /// <value>The display text.</value>
        public abstract string DisplayText { get; internal set; }

        /// <summary>
        ///     Gets the sos string.
        /// </summary>
        /// <value>The sos string.</value>
        public abstract string SosString { get; internal set; }

        /// <summary>
        ///     Class ActiveAppDomainStage.
        /// </summary>
        /// <seealso cref="Triage.Mortician.AppDomainStage" />
        public class ActiveAppDomainStage : AppDomainStage
        {
            /// <summary>
            ///     Gets the display text.
            /// </summary>
            /// <value>The display text.</value>
            /// <inheritdoc />
            public override string DisplayText { get; internal set; } = "Active";

            /// <summary>
            ///     Gets the sos string.
            /// </summary>
            /// <value>The sos string.</value>
            /// <inheritdoc />
            public override string SosString { get; internal set; } = "ACTIVE";
        }

        /// <summary>
        ///     Class ClearedAppDomainStage.
        /// </summary>
        /// <seealso cref="Triage.Mortician.AppDomainStage" />
        public class ClearedAppDomainStage : AppDomainStage
        {
            /// <summary>
            ///     Gets the display text.
            /// </summary>
            /// <value>The display text.</value>
            /// <inheritdoc />
            public override string DisplayText { get; internal set; } = "Cleared";

            /// <summary>
            ///     Gets the sos string.
            /// </summary>
            /// <value>The sos string.</value>
            /// <inheritdoc />
            public override string SosString { get; internal set; } = "CLEARED";
        }

        /// <summary>
        ///     Class ClosedAppDomainStage.
        /// </summary>
        /// <seealso cref="Triage.Mortician.AppDomainStage" />
        public class ClosedAppDomainStage : AppDomainStage
        {
            /// <summary>
            ///     Gets the display text.
            /// </summary>
            /// <value>The display text.</value>
            /// <inheritdoc />
            public override string DisplayText { get; internal set; } = "Closed";

            /// <summary>
            ///     Gets the sos string.
            /// </summary>
            /// <value>The sos string.</value>
            /// <inheritdoc />
            public override string SosString { get; internal set; } = "CLOSED";
        }

        /// <summary>
        ///     Class CollectedAppDomainStage.
        /// </summary>
        /// <seealso cref="Triage.Mortician.AppDomainStage" />
        public class CollectedAppDomainStage : AppDomainStage
        {
            /// <summary>
            ///     Gets the display text.
            /// </summary>
            /// <value>The display text.</value>
            /// <inheritdoc />
            public override string DisplayText { get; internal set; } = "Collected";

            /// <summary>
            ///     Gets the sos string.
            /// </summary>
            /// <value>The sos string.</value>
            /// <inheritdoc />
            public override string SosString { get; internal set; } = "COLLECTED";
        }

        /// <summary>
        ///     Class CreatingAppDomainStage.
        /// </summary>
        /// <seealso cref="Triage.Mortician.AppDomainStage" />
        public class CreatingAppDomainStage : AppDomainStage
        {
            /// <summary>
            ///     Gets the display text.
            /// </summary>
            /// <value>The display text.</value>
            /// <inheritdoc />
            public override string DisplayText { get; internal set; } = "Creating";

            /// <summary>
            ///     Gets the sos string.
            /// </summary>
            /// <value>The sos string.</value>
            /// <inheritdoc />
            public override string SosString { get; internal set; } = "CREATING";
        }

        /// <summary>
        ///     Class ExitedAppDomainStage.
        /// </summary>
        /// <seealso cref="Triage.Mortician.AppDomainStage" />
        public class ExitedAppDomainStage : AppDomainStage
        {
            /// <summary>
            ///     Gets the display text.
            /// </summary>
            /// <value>The display text.</value>
            /// <inheritdoc />
            public override string DisplayText { get; internal set; } = "Exited";

            /// <summary>
            ///     Gets the sos string.
            /// </summary>
            /// <value>The sos string.</value>
            /// <inheritdoc />
            public override string SosString { get; internal set; } = "EXITED";
        }

        /// <summary>
        ///     Class ExitingAppDomainStage.
        /// </summary>
        /// <seealso cref="Triage.Mortician.AppDomainStage" />
        public class ExitingAppDomainStage : AppDomainStage
        {
            /// <summary>
            ///     Gets the display text.
            /// </summary>
            /// <value>The display text.</value>
            /// <inheritdoc />
            public override string DisplayText { get; internal set; } = "Exiting";

            /// <summary>
            ///     Gets the sos string.
            /// </summary>
            /// <value>The sos string.</value>
            /// <inheritdoc />
            public override string SosString { get; internal set; } = "EXITING";
        }

        /// <summary>
        ///     Class FinalizedAppDomainStage.
        /// </summary>
        /// <seealso cref="Triage.Mortician.AppDomainStage" />
        public class FinalizedAppDomainStage : AppDomainStage
        {
            /// <summary>
            ///     Gets the display text.
            /// </summary>
            /// <value>The display text.</value>
            /// <inheritdoc />
            public override string DisplayText { get; internal set; } = "Finalized";

            /// <summary>
            ///     Gets the sos string.
            /// </summary>
            /// <value>The sos string.</value>
            /// <inheritdoc />
            public override string SosString { get; internal set; } = "FINALIZED";
        }

        /// <summary>
        ///     Class FinalizingAppDomainStage.
        /// </summary>
        /// <seealso cref="Triage.Mortician.AppDomainStage" />
        public class FinalizingAppDomainStage : AppDomainStage
        {
            /// <summary>
            ///     Gets the display text.
            /// </summary>
            /// <value>The display text.</value>
            /// <inheritdoc />
            public override string DisplayText { get; internal set; } = "Finalizing";

            /// <summary>
            ///     Gets the sos string.
            /// </summary>
            /// <value>The sos string.</value>
            /// <inheritdoc />
            public override string SosString { get; internal set; } = "FINALIZING";
        }

        /// <summary>
        ///     Class HandleTableNoAccessAppDomainStage.
        /// </summary>
        /// <seealso cref="Triage.Mortician.AppDomainStage" />
        public class HandleTableNoAccessAppDomainStage : AppDomainStage
        {
            /// <summary>
            ///     Gets the display text.
            /// </summary>
            /// <value>The display text.</value>
            /// <inheritdoc />
            public override string DisplayText { get; internal set; } = "Handle Table No Access";

            /// <summary>
            ///     Gets the sos string.
            /// </summary>
            /// <value>The sos string.</value>
            /// <inheritdoc />
            public override string SosString { get; internal set; } = "HANDLETABLE_NOACCESS";
        }

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

            /// <summary>
            ///     Gets the sos string.
            /// </summary>
            /// <value>The sos string.</value>
            /// <inheritdoc />
            public override string SosString { get; internal set; } = "OPEN";
        }

        /// <summary>
        ///     Class ReadyForManagedCodeAppDomainStage.
        /// </summary>
        /// <seealso cref="Triage.Mortician.AppDomainStage" />
        public class ReadyForManagedCodeAppDomainStage : AppDomainStage
        {
            /// <summary>
            ///     Gets the display text.
            /// </summary>
            /// <value>The display text.</value>
            /// <inheritdoc />
            public override string DisplayText { get; internal set; } = "Ready For Managed Code";

            /// <summary>
            ///     Gets the sos string.
            /// </summary>
            /// <value>The sos string.</value>
            /// <inheritdoc />
            public override string SosString { get; internal set; } = "READYFORMANAGEDCODE";
        }

        /// <summary>
        ///     Class UnknownAppDomainStage.
        /// </summary>
        /// <seealso cref="Triage.Mortician.AppDomainStage" />
        public class UnknownAppDomainStage : AppDomainStage
        {
            /// <summary>
            ///     Gets the display text.
            /// </summary>
            /// <value>The display text.</value>
            /// <inheritdoc />
            public override string DisplayText { get; internal set; } = "Unknown";

            /// <summary>
            ///     Gets the sos string.
            /// </summary>
            /// <value>The sos string.</value>
            /// <inheritdoc />
            public override string SosString { get; internal set; } = "UNKNOWN";
        }

        /// <summary>
        ///     Class UnloadRequestedAppDomainStage.
        /// </summary>
        /// <seealso cref="Triage.Mortician.AppDomainStage" />
        public class UnloadRequestedAppDomainStage : AppDomainStage
        {
            /// <summary>
            ///     Gets the display text.
            /// </summary>
            /// <value>The display text.</value>
            /// <inheritdoc />
            public override string DisplayText { get; internal set; } = "Unload Requested";

            /// <summary>
            ///     Gets the sos string.
            /// </summary>
            /// <value>The sos string.</value>
            /// <inheritdoc />
            public override string SosString { get; internal set; } = "UNLOAD_REQUESTED";
        }
    }
}