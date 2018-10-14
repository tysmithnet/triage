using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Mortician.Core.ClrMdAbstractions;

namespace Testing.Common
{
    public class SymbolLocatorBuilder : Builder<ISymbolLocator>
    {
        public SymbolLocatorBuilder WithSymbolCache(string cache)
        {
            Mock.Setup(locator => locator.SymbolCache).Returns(cache);
            return this;
        }

        public SymbolLocatorBuilder WithSymbolPath(string path)
        {
            Mock.Setup(locator => locator.SymbolPath).Returns(path);
            return this;
        }
    }
}
