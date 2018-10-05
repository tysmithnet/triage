using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Mortician.Core.ClrMdAbstractions;

namespace Testing.Common
{
    public class ClrThreadPoolBuilder : Builder<IClrThreadPool>
    {
        public ClrThreadPoolBuilder WithCpuUtilization(int percent)
        {
            Mock.Setup(pool => pool.CpuUtilization).Returns(percent);
            return this;
        }
        
        public ClrThreadPoolBuilder WithMaxFreeCompletionPorts(int max)
        {
            Mock.Setup(pool => pool.MaxFreeCompletionPorts).Returns(max);
            return this;
        }

        public ClrThreadPoolBuilder WithMaxCompletionPorts(int max)
        {
            Mock.Setup(pool => pool.MaxCompletionPorts).Returns(max);
            return this;
        }

        public ClrThreadPoolBuilder WithMaxThreads(int max)
        {
            Mock.Setup(pool => pool.MaxThreads).Returns(max);
            return this;
        }

        public ClrThreadPoolBuilder WithMinCompletionPorts(int min)
        {
            Mock.Setup(pool => pool.MinCompletionPorts).Returns(min);
            return this;
        }

        public ClrThreadPoolBuilder WithMinThreads(int min)
        {
            Mock.Setup(pool => pool.MinThreads).Returns(min);
            return this;
        }

        public ClrThreadPoolBuilder WithFreeCompletionPortCount(int free)
        {
            Mock.Setup(pool => pool.FreeCompletionPortCount).Returns(free);
            return this;
        }

        public ClrThreadPoolBuilder WithIdleThreads(int num)
        {
            Mock.Setup(pool => pool.IdleThreads).Returns(num);
            return this;
        }

        public ClrThreadPoolBuilder WithRunningThreads(int num)
        {
            Mock.Setup(pool => pool.RunningThreads).Returns(num);
            return this;
        }

        public ClrThreadPoolBuilder WithTotalThreads(int total)
        {
            Mock.Setup(pool => pool.TotalThreads).Returns(total);
            return this;
        }
    }
}
