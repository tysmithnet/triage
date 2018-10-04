using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Moq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Testing.Common
{
    [ExcludeFromCodeCoverage]
    public class DataTargetBuilder : Builder<IDataTarget>
    {
        public DataTargetBuilder WithArchitecture(Architecture architecture)
        {
            Mock.Setup(target => target.Architecture).Returns(architecture);
            return this;
        }

        public DataTargetBuilder WithClrVersions(IList<IClrInfo> clrVersions)
        {
            Mock.Setup(target => target.ClrVersions).Returns(clrVersions);
            return this;
        }

        public DataTargetBuilder WithDataReader(IDataReader dataReader)
        {
            Mock.Setup(target => target.DataReader).Returns(dataReader);
            return this;
        }

        public DataTargetBuilder WithIsMinidump(bool isMiniDump)
        {
            Mock.Setup(target => target.IsMinidump).Returns(isMiniDump);
            return this;
        }

        public DataTargetBuilder WithPointerSize(uint size)
        {
            Mock.Setup(target => target.PointerSize).Returns(size);
            return this;
        }

        public DataTargetBuilder WithSymbolLocator(ISymbolLocator symbolLocator)
        {
            Mock.Setup(target => target.SymbolLocator).Returns(symbolLocator);
            return this;
        }

        public DataTargetBuilder WithSymbolProvider(ISymbolProvider symbolProvider)
        {
            Mock.Setup(target => target.SymbolProvider).Returns(symbolProvider);
            return this;
        }

        public DataTargetBuilder WithEnumerateModules(IEnumerable<IModuleInfo> moduleInfos)
        {
            Mock.Setup(target => target.EnumerateModules()).Returns(moduleInfos);
            return this;
        }
    }
}