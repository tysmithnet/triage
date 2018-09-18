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
            _locator = locator ?? throw new ArgumentNullException(nameof(locator));
        }

        internal SymbolLocator _locator;

        /// <inheritdoc />
        public string FindBinary(string fileName, uint buildTimeStamp, uint imageSize, bool checkProperties = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string FindBinary(string fileName, int buildTimeStamp, int imageSize, bool checkProperties = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string FindBinary(IModuleInfo module, bool checkProperties = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string FindBinary(IDacInfo dac)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> FindBinaryAsync(string fileName, uint buildTimeStamp, uint imageSize,
            bool checkProperties = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> FindBinaryAsync(string fileName, int buildTimeStamp, int imageSize,
            bool checkProperties = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> FindBinaryAsync(IModuleInfo module, bool checkProperties = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> FindBinaryAsync(IDacInfo dac)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string FindPdb(IModuleInfo module)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string FindPdb(IPdbInfo pdb)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string FindPdb(string pdbName, Guid pdbIndexGuid, int pdbIndexAge)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> FindPdbAsync(IModuleInfo module)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> FindPdbAsync(IPdbInfo pdb)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> FindPdbAsync(string pdbName, Guid pdbIndexGuid, int pdbIndexAge)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string SymbolCache { get; set; }

        /// <inheritdoc />
        public string SymbolPath { get; set; }

        /// <inheritdoc />
        public int Timeout { get; set; }
    }
}