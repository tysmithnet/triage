using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mortician.Core;

namespace Testing.Common
{
    public class DumpThreadRepositoryBuilder : Builder<IDumpThreadRepository>
    {
        public DumpThreadRepositoryBuilder WithThreads(IEnumerable<DumpThread> threads)
        {
            Mock.Setup(repository => repository.Threads).Returns(threads);
            return this;
        }
    }
}
