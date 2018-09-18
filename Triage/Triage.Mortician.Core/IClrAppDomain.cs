using System.Collections.Generic;

namespace Triage.Mortician.Core
{
    public interface IClrAppDomain
    {
        /// <summary>
        /// Gets the runtime associated with this ClrAppDomain.
        /// </summary>
        ClrRuntime Runtime { get; }

        /// <summary>
        /// Address of the AppDomain.
        /// </summary>
        ulong Address { get; }

        /// <summary>
        /// The AppDomain's ID.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// The name of the AppDomain, as specified when the domain was created.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns a list of modules loaded into this AppDomain.
        /// </summary>
        IList<ClrModule> Modules { get; }

        /// <summary>
        /// Returns the config file used for the AppDomain.  This may be null if there was no config file
        /// loaded, or if the targeted runtime does not support enumerating that data.
        /// </summary>
        string ConfigurationFile { get; }

        /// <summary>
        /// Returns the base directory for this AppDomain.  This may return null if the targeted runtime does
        /// not support enumerating this information.
        /// </summary>
        string ApplicationBase { get; }

        /// <summary>
        /// To string override.
        /// </summary>
        /// <returns>The name of this AppDomain.</returns>
        string ToString();
    }
}