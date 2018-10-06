using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Common;
using Moq;
using Mortician.Core.ClrMdAbstractions;
using Mortician.Repositories;
using Testing.Common;
using Xunit;

namespace Mortician.Test.Repositories
{
    public class DumpInformationRepository_Should : BaseTest
    {
        [Fact]
        public void Return_The_Correct_Values()
        {
            // arrange
            var startTime = DateTime.Now;
            var moduleInfo = new Mock<IModuleInfo>();
            var moduleInfos = new[] {moduleInfo.Object};
            var locator = new SymbolLocatorBuilder()
                .WithSymbolCache(@"c:\cache")
                .WithSymbolPath(@"c:\symbols")
                .Build();

            var heap = new ClrHeapBuilder()
                .WithTotalSize(0x100)
                .Build();

            var pool = new ClrThreadPoolBuilder()
                .WithCpuUtilization(85)
                .WithFreeCompletionPortCount(90)
                .WithIdleThreads(11)
                .WithMaxCompletionPorts(20)
                .WithMaxFreeCompletionPorts(50)
                .WithMaxThreads(100)
                .WithMinCompletionPorts(8)
                .WithMinThreads(12)
                .WithRunningThreads(22)
                .WithTotalThreads(123)
                .Build();

            var clrVersion = new Mock<IClrInfo>();
            var runtime = new ClrRuntimeBuilder()
                .WithHeap(heap)
                .WithHeapCount(3)
                .WithServerGc(true)
                .WithThreadPool(pool)
                .Build();

            var dataReader = new Mock<IDataReader>();
            var dataTarget = new DataTargetBuilder()
                .WithArchitecture(Architecture.Amd64)
                .WithClrVersions(new List<IClrInfo>
                {
                    clrVersion.Object
                })
                .WithDataReader(dataReader.Object)
                .WithIsMinidump(true)
                .WithPointerSize(4)
                .WithSymbolLocator(locator)
                .WithEnumerateModules(moduleInfos)
                .Build();
            var fileInfo = new FileInfo(@"C:\dumps\crash.dmp");
            var sut = new DumpInformationRepository(dataTarget, runtime, fileInfo);

            // act
            // assert
            sut.CpuUtilization.Should().Be(85);
            sut.DumpFile.Should().Be(fileInfo);
            sut.HeapCount.Should().Be(3);
            sut.IsMiniDump.Should().Be(true);
            sut.IsServerGc.Should().Be(true);
            sut.MaxNumberFreeIoCompletionPorts.Should().Be(50);
            sut.MaxNumberIoCompletionPorts.Should().Be(20);
            sut.MaxThreads.Should().Be(100);
            sut.MinNumberIoCompletionPorts.Should().Be(8);
            sut.MinThreads.Should().Be(12);
            sut.NumberFreeIoCompletionPorts.Should().Be(90);
            sut.NumberIdleThreads.Should().Be(11);
            sut.NumRunningThreads.Should().Be(22);
            sut.ModuleInfos.SequenceEqual(moduleInfos).Should().BeTrue();
            sut.SymbolCache.Should().Be(@"c:\cache");
            sut.SymbolPath.Should().Be(@"c:\symbols");
            sut.TotalHeapSize.Should().Be(0x100);
            sut.TotalThreads.Should().Be(123);
            sut.StartTimeUtc.Should().BeCloseTo(startTime.ToUniversalTime(), 1000);
        }

        [Fact]
        public void Provide_A_Default_Internal_Constructor()
        {
            // arrange
            Action mightThrow = () => new DumpInformationRepository();
            
            // act
            // assert
            mightThrow.Should().NotThrow();
        }
    }
}