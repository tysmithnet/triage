using System;
using System.Threading.Tasks;
using Microsoft.Diagnostics.Runtime.Utilities;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class SymbolLocatorAdapter : ISymbolLocator
    {
        /// <inheritdoc />
        public SymbolLocatorAdapter(SymbolLocator locator)
        {
            Locator = locator ?? throw new ArgumentNullException(nameof(locator));
        }

        internal SymbolLocator Locator;

        /// <inheritdoc />
        public string FindBinary(string fileName, uint buildTimeStamp, uint imageSize, bool checkProperties = true) => Locator.FindBinary(fileName, buildTimeStamp, imageSize, checkProperties);

        /// <inheritdoc />
        public string FindBinary(string fileName, int buildTimeStamp, int imageSize, bool checkProperties = true) => Locator.FindBinary(fileName, buildTimeStamp, imageSize, checkProperties);

        /// <inheritdoc />
        public string FindBinary(IModuleInfo module, bool checkProperties = true) => Locator.FindBinary((module as ModuleInfoAdapter)?.ModuleInfo, checkProperties);

        /// <inheritdoc />
        public string FindBinary(IDacInfo dac) => Locator.FindBinary((dac as DacInfoAdapter)?.DacInfo);

        /// <inheritdoc />
        public Task<string> FindBinaryAsync(string fileName, uint buildTimeStamp, uint imageSize, bool checkProperties = true) => Locator.FindBinaryAsync(fileName, buildTimeStamp, imageSize, checkProperties);

        /// <inheritdoc />
        public Task<string> FindBinaryAsync(string fileName, int buildTimeStamp, int imageSize, bool checkProperties = true) => Locator.FindBinaryAsync(fileName, buildTimeStamp, imageSize, checkProperties);

        /// <inheritdoc />
        public Task<string> FindBinaryAsync(IModuleInfo module, bool checkProperties = true) => Locator.FindBinaryAsync((module as ModuleInfoAdapter)?.ModuleInfo, checkProperties);

        /// <inheritdoc />
        public Task<string> FindBinaryAsync(IDacInfo dac) => Locator.FindBinaryAsync((dac as DacInfoAdapter)?.DacInfo);

        /// <inheritdoc />
        public string FindPdb(IModuleInfo module) => Locator.FindPdb((module as ModuleInfoAdapter)?.ModuleInfo);

        /// <inheritdoc />
        public string FindPdb(IPdbInfo pdb) => Locator.FindPdb((pdb as PdbInfoAdapter)?.PdbInfo);

        /// <inheritdoc />
        public string FindPdb(string pdbName, Guid pdbIndexGuid, int pdbIndexAge) =>
            Locator.FindPdb(pdbName, pdbIndexGuid, pdbIndexAge);

        /// <inheritdoc />
        public Task<string> FindPdbAsync(IModuleInfo module) => Locator.FindPdbAsync((module as ModuleInfoAdapter)?.ModuleInfo);

        /// <inheritdoc />
        public Task<string> FindPdbAsync(IPdbInfo pdb) => Locator.FindPdbAsync((pdb as PdbInfoAdapter)?.PdbInfo);

        /// <inheritdoc />
        public Task<string> FindPdbAsync(string pdbName, Guid pdbIndexGuid, int pdbIndexAge) =>
            Locator.FindPdbAsync(pdbName, pdbIndexGuid, pdbIndexAge);

        /// <inheritdoc />
        public string SymbolCache => Locator.SymbolCache;

        /// <inheritdoc />
        public string SymbolPath => Locator.SymbolPath;

        /// <inheritdoc />
        public int Timeout => Locator.Timeout;
    }
}