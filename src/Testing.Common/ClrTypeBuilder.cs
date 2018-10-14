using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Mortician.Core.ClrMdAbstractions;

namespace Testing.Common
{
    public class ClrTypeBuilder : Builder<IClrType>
    {
        public ClrTypeBuilder WithName(string name)
        {
            Mock.Setup(type => type.Name).Returns(name);
            return this;
        }

        public ClrTypeBuilder WithGetSize(ulong size)
        {
            Mock.Setup(type => type.GetSize(It.IsAny<ulong>())).Returns(size);
            return this;
        }
    }
}
