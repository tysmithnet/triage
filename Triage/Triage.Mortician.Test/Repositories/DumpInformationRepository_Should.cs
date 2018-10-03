using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Triage.Mortician.Core.ClrMdAbstractions;
using Triage.Mortician.Repositories;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Test.Repositories
{
    public class DumpInformationRepository_Should : BaseTest
    {
        [Fact]
        public void Have_The_Correct_Information()
        {
            // arrange
            var dt = new Mock<IDataTarget>();
            var rt = new Mock<IClrRuntime>();
            var fi = new FileInfo(@"C:\dumps\some.dmp");
            var repo = new DumpInformationRepository(dt.Object, rt.Object, fi);

            // act
            // assert
        }
    }
}
