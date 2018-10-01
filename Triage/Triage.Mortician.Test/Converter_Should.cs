using FluentAssertions;
using Triage.Mortician.Core.ClrMdAbstractions;
using Triage.Testing.Common;
using Xunit;
using ClrMd = Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician.Test
{
    public class Converter_Should : BaseTest
    {
        [Fact]
        public void Convert_Architecture()
        {
            // arrange
            var sut = new Converter();

            // act
            // assert
            sut.Convert(ClrMd.Architecture.Unknown).Should().Be(Architecture.Unknown);
            sut.Convert(ClrMd.Architecture.X86).Should().Be(Architecture.X86);
            sut.Convert(ClrMd.Architecture.Amd64).Should().Be(Architecture.Amd64);
            sut.Convert(ClrMd.Architecture.Arm).Should().Be(Architecture.Arm);
        }

        [Fact]
        public void Convert_Blocking_Reason()
        {
            // arrange
            var sut = new Converter();

            // act
            // assert
            sut.Convert(ClrMd.BlockingReason.None).Should().Be(BlockingReason.None);
            sut.Convert(ClrMd.BlockingReason.Unknown).Should().Be(BlockingReason.Unknown);
            sut.Convert(ClrMd.BlockingReason.Monitor).Should().Be(BlockingReason.Monitor);
            sut.Convert(ClrMd.BlockingReason.MonitorWait).Should().Be(BlockingReason.MonitorWait);
            sut.Convert(ClrMd.BlockingReason.ReaderAcquired).Should().Be(BlockingReason.ReaderAcquired);
            sut.Convert(ClrMd.BlockingReason.ThreadJoin).Should().Be(BlockingReason.ThreadJoin);
            sut.Convert(ClrMd.BlockingReason.WaitAll).Should().Be(BlockingReason.WaitAll);
            sut.Convert(ClrMd.BlockingReason.WaitOne).Should().Be(BlockingReason.WaitOne);
            sut.Convert(ClrMd.BlockingReason.WriterAcquired).Should().Be(BlockingReason.WriterAcquired);
        }

        [Fact]
        public void Convert_Element_Type()
        {
            // arrange
            var sut = new Converter();

            // act
            // assert
            sut.Convert(ClrMd.ClrElementType.Unknown).Should().Be(ClrElementType.Unknown);
            sut.Convert(ClrMd.ClrElementType.Boolean).Should().Be(ClrElementType.Boolean);
            sut.Convert(ClrMd.ClrElementType.Char).Should().Be(ClrElementType.Char);
            sut.Convert(ClrMd.ClrElementType.Int8).Should().Be(ClrElementType.Int8);
            sut.Convert(ClrMd.ClrElementType.UInt8).Should().Be(ClrElementType.UInt8);
            sut.Convert(ClrMd.ClrElementType.Int16).Should().Be(ClrElementType.Int16);
            sut.Convert(ClrMd.ClrElementType.UInt16).Should().Be(ClrElementType.UInt16);
            sut.Convert(ClrMd.ClrElementType.Int32).Should().Be(ClrElementType.Int32);
            sut.Convert(ClrMd.ClrElementType.UInt32).Should().Be(ClrElementType.UInt32);
            sut.Convert(ClrMd.ClrElementType.Int64).Should().Be(ClrElementType.Int64);
            sut.Convert(ClrMd.ClrElementType.UInt64).Should().Be(ClrElementType.UInt64);
            sut.Convert(ClrMd.ClrElementType.Float).Should().Be(ClrElementType.Float);
            sut.Convert(ClrMd.ClrElementType.Double).Should().Be(ClrElementType.Double);
            sut.Convert(ClrMd.ClrElementType.String).Should().Be(ClrElementType.String);
            sut.Convert(ClrMd.ClrElementType.Pointer).Should().Be(ClrElementType.Pointer);
            sut.Convert(ClrMd.ClrElementType.Struct).Should().Be(ClrElementType.Struct);
            sut.Convert(ClrMd.ClrElementType.Class).Should().Be(ClrElementType.Class);
            sut.Convert(ClrMd.ClrElementType.Array).Should().Be(ClrElementType.Array);
            sut.Convert(ClrMd.ClrElementType.NativeInt).Should().Be(ClrElementType.NativeInt);
            sut.Convert(ClrMd.ClrElementType.NativeUInt).Should().Be(ClrElementType.NativeUInt);
            sut.Convert(ClrMd.ClrElementType.FunctionPointer).Should().Be(ClrElementType.FunctionPointer);
            sut.Convert(ClrMd.ClrElementType.Object).Should().Be(ClrElementType.Object);
            sut.Convert(ClrMd.ClrElementType.SZArray).Should().Be(ClrElementType.SZArray);
        }

        [Fact]
        public void Convert_Gc_Mode()
        {
            // arrange
            var sut = new Converter();

            // act
            // assert
            sut.Convert(ClrMd.GcMode.Cooperative).Should().Be(GcMode.Cooperative);
            sut.Convert(ClrMd.GcMode.Preemptive).Should().Be(GcMode.Preemptive);
        }

        [Fact]
        public void Convert_Gc_Root_Kind()
        {
            // arrange
            var sut = new Converter();

            // act
            // assert
            sut.Convert(ClrMd.GCRootKind.StaticVar).Should().Be(GcRootKind.StaticVar);
            sut.Convert(ClrMd.GCRootKind.ThreadStaticVar).Should().Be(GcRootKind.ThreadStaticVar);
            sut.Convert(ClrMd.GCRootKind.LocalVar).Should().Be(GcRootKind.LocalVar);
            sut.Convert(ClrMd.GCRootKind.Strong).Should().Be(GcRootKind.Strong);
            sut.Convert(ClrMd.GCRootKind.Weak).Should().Be(GcRootKind.Weak);
            sut.Convert(ClrMd.GCRootKind.Pinning).Should().Be(GcRootKind.Pinning);
            sut.Convert(ClrMd.GCRootKind.Finalizer).Should().Be(GcRootKind.Finalizer);
            sut.Convert(ClrMd.GCRootKind.AsyncPinning).Should().Be(GcRootKind.AsyncPinning);
        }

        [Fact]
        public void Convert_GCSegmentType()
        {
            // arrange
            var sut = new Converter();

            // act
            // assert
            sut.Convert(ClrMd.GCSegmentType.Ephemeral).Should().Be(GcSegmentType.Ephemeral);
            sut.Convert(ClrMd.GCSegmentType.LargeObject).Should().Be(GcSegmentType.LargeObject);
            sut.Convert(ClrMd.GCSegmentType.Regular).Should().Be(GcSegmentType.Regular);
        }

        [Fact]
        public void Convert_Handle_Type()
        {
            // arrange
            var sut = new Converter();

            // act
            // assert
            sut.Convert(ClrMd.HandleType.WeakShort).Should().Be(HandleType.WeakShort);
            sut.Convert(ClrMd.HandleType.WeakLong).Should().Be(HandleType.WeakLong);
            sut.Convert(ClrMd.HandleType.Strong).Should().Be(HandleType.Strong);
            sut.Convert(ClrMd.HandleType.Pinned).Should().Be(HandleType.Pinned);
            sut.Convert(ClrMd.HandleType.RefCount).Should().Be(HandleType.RefCount);
            sut.Convert(ClrMd.HandleType.Dependent).Should().Be(HandleType.Dependent);
            sut.Convert(ClrMd.HandleType.AsyncPinned).Should().Be(HandleType.AsyncPinned);
            sut.Convert(ClrMd.HandleType.SizedRef).Should().Be(HandleType.SizedRef);
        }

        [Fact]
        public void Convert_Memory_Region_Type()
        {
            // arrange
            var sut = new Converter();

            // act
            // assert
            sut.Convert(ClrMd.ClrMemoryRegionType.LowFrequencyLoaderHeap).Should()
                .Be(ClrMemoryRegionType.LowFrequencyLoaderHeap);
            sut.Convert(ClrMd.ClrMemoryRegionType.HighFrequencyLoaderHeap).Should()
                .Be(ClrMemoryRegionType.HighFrequencyLoaderHeap);
            sut.Convert(ClrMd.ClrMemoryRegionType.StubHeap).Should().Be(ClrMemoryRegionType.StubHeap);
            sut.Convert(ClrMd.ClrMemoryRegionType.IndcellHeap).Should().Be(ClrMemoryRegionType.IndcellHeap);
            sut.Convert(ClrMd.ClrMemoryRegionType.LookupHeap).Should().Be(ClrMemoryRegionType.LookupHeap);
            sut.Convert(ClrMd.ClrMemoryRegionType.ResolveHeap).Should().Be(ClrMemoryRegionType.ResolveHeap);
            sut.Convert(ClrMd.ClrMemoryRegionType.DispatchHeap).Should().Be(ClrMemoryRegionType.DispatchHeap);
            sut.Convert(ClrMd.ClrMemoryRegionType.CacheEntryHeap).Should().Be(ClrMemoryRegionType.CacheEntryHeap);
            sut.Convert(ClrMd.ClrMemoryRegionType.JitHostCodeHeap).Should().Be(ClrMemoryRegionType.JitHostCodeHeap);
            sut.Convert(ClrMd.ClrMemoryRegionType.JitLoaderCodeHeap).Should().Be(ClrMemoryRegionType.JitLoaderCodeHeap);
            sut.Convert(ClrMd.ClrMemoryRegionType.ModuleThunkHeap).Should().Be(ClrMemoryRegionType.ModuleThunkHeap);
            sut.Convert(ClrMd.ClrMemoryRegionType.ModuleLookupTableHeap).Should()
                .Be(ClrMemoryRegionType.ModuleLookupTableHeap);
            sut.Convert(ClrMd.ClrMemoryRegionType.GCSegment).Should().Be(ClrMemoryRegionType.GCSegment);
            sut.Convert(ClrMd.ClrMemoryRegionType.ReservedGCSegment).Should().Be(ClrMemoryRegionType.ReservedGCSegment);
            sut.Convert(ClrMd.ClrMemoryRegionType.HandleTableChunk).Should().Be(ClrMemoryRegionType.HandleTableChunk);
        }

        [Fact]
        public void Convert_Root_Stackwalk_Policy()
        {
            // arrange
            var sut = new Converter();

            // act
            // assert
            sut.Convert(ClrMd.ClrRootStackwalkPolicy.Automatic).Should().Be(ClrRootStackwalkPolicy.Automatic);
            sut.Convert(ClrMd.ClrRootStackwalkPolicy.Exact).Should().Be(ClrRootStackwalkPolicy.Exact);
            sut.Convert(ClrMd.ClrRootStackwalkPolicy.Fast).Should().Be(ClrRootStackwalkPolicy.Fast);
            sut.Convert(ClrMd.ClrRootStackwalkPolicy.SkipStack).Should().Be(ClrRootStackwalkPolicy.SkipStack);
        }

        [Fact]
        public void Convert_Stack_Frame_Type()
        {
            // arrange
            var sut = new Converter();

            // act
            // assert
            sut.Convert(ClrMd.ClrStackFrameType.Unknown).Should().Be(ClrStackFrameType.Unknown);
            sut.Convert(ClrMd.ClrStackFrameType.ManagedMethod).Should().Be(ClrStackFrameType.ManagedMethod);
            sut.Convert(ClrMd.ClrStackFrameType.Runtime).Should().Be(ClrStackFrameType.Runtime);
        }

        [Fact]
        public void Convert_WorkItem_Kind()
        {
            // arrange
            var sut = new Converter();

            // act
            // assert
            sut.Convert(ClrMd.WorkItemKind.Unknown).Should().Be(WorkItemKind.Unknown);
            sut.Convert(ClrMd.WorkItemKind.AsyncTimer).Should().Be(WorkItemKind.AsyncTimer);
            sut.Convert(ClrMd.WorkItemKind.AsyncCallback).Should().Be(WorkItemKind.AsyncCallback);
            sut.Convert(ClrMd.WorkItemKind.QueueUserWorkItem).Should().Be(WorkItemKind.QueueUserWorkItem);
            sut.Convert(ClrMd.WorkItemKind.TimerDelete).Should().Be(WorkItemKind.TimerDelete);
        }
    }
}