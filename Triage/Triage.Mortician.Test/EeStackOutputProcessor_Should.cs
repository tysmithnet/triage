using System.Linq;
using FluentAssertions;
using Triage.Mortician.Core;
using Triage.Mortician.Reports;
using Triage.Mortician.Reports.EeStack;
using Xunit;
using Xunit.Sdk;

namespace Triage.Mortician.Test
{
    public class EeStackOutputProcessor_Should
    {
        [Fact]
        public void Extract_The_Correct_Number_Of_Items()
        {
            // arrange
            var processor = new EeStackReportFactory();

            // act
            var report = processor.ProcessOutput(HELLO_WORLD);

            // assert
            report.ThreadsInternal.Count.Should().Be(2);
            report.Threads.ElementAt(0).StackFramesInternal.Should().HaveCount(105);
            report.Threads.ElementAt(1).StackFramesInternal.Should().HaveCount(27);
        }

        [Fact]
        public void Extract_The_Correct_Number_Of_Items2()
        {
            // arrange
            var processor = new EeStackReportFactory();

            // act
            var report = processor.ProcessOutput(IIS_EXAMPLE);

            // assert
            report.ThreadsInternal.Count.Should().Be(17);
        }

        [Fact]
        public void Extract_Threads_With_Errors_In_Them()
        {
            // arrange
            string textWithErrors = @"Thread   6
Current frame: ntdll!NtRemoveIoCompletion+0x14
Child-SP         RetAddr          Caller, Callee
000000f66b77fa40 00007fffb53c9a8f KERNELBASE!GetQueuedCompletionStatus+0x3f, calling ntdll!NtRemoveIoCompletion
000000f66b77fa60 00007fff94c14e42 *** ERROR: Symbol file could not be found.  Defaulted to export symbols for w3dt.dll - 
w3dt!HTTP_WRAPPER::QueryState+0x3df2, calling ntdll!LdrpDispatchUserCallTarget
000000f66b77faa0 00007fff9d962b46 *** ERROR: Symbol file could not be found.  Defaulted to export symbols for w3tp.dll - 
w3tp!THREAD_POOL::PostCompletion+0x86, calling kernel32!GetQueuedCompletionStatusStub
000000f66b77fb00 00007fff9d961819 w3tp+0x1819, calling ntdll!LdrpDispatchUserCallTarget
000000f66b77fb40 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66b77fb70 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
";
            var processor = new EeStackReportFactory();

            // act
            var thread = processor.ExtractThread(textWithErrors);

            // assert
            thread.Index.Should().Be(6);
            thread.StackFrames.Should().HaveCount(6);
        }
        [Fact]
        public void Extract_Managed_Calling_Native()
        {
            // arrange
            var frameText = @"000000f66cabeaa0 00007fff9ef6706a (MethodDesc 00007fff9ebc6a90 +0x1a System.MulticastDelegate.CtorOpened(System.Object, IntPtr, IntPtr)), calling clr!JIT_WriteBarrier";
            var processor = new EeStackReportFactory();

            // act
            var result = processor.ExtractFrame(frameText);
            var caller = result.Caller as ManagedCodeLocation;
            

            // assert
            caller.Should().NotBeNull();
            result.ChildStackPointer.Should().Be(0x000000f66cabeaa0);
            result.ReturnAddress.Should().Be(0x00007fff9ef6706a);

            caller.MethodDescriptor.Should().Be(0x00007fff9ebc6a90);
            caller.Method.Should().Be("System.MulticastDelegate.CtorOpened(System.Object, IntPtr, IntPtr)");
            caller.Offset.Should().Be(0x1a);
            
            result.Callee.Module.Should().Be("clr");
            result.Callee.Method.Should().Be("JIT_WriteBarrier");
            result.Callee.Offset.Should().Be(0);
        }

        [Fact]
        public void Extract_Native_Calling_Native()
        {
            // arrange
            var frameText = @"000000056e7cc800 00007ffc330dcc3a ntdll!LdrpLoadResourceFromAlternativeModule+0x2a2, calling ntdll!_security_check_cookie";
            var processor = new EeStackReportFactory();

            // act
            var result = processor.ExtractFrame(frameText);

            // assert
            result.ChildStackPointer.Should().Be(0x000000056e7cc800);
            result.ReturnAddress.Should().Be(0x00007ffc330dcc3a);
            result.Caller.Module.Should().Be("ntdll");
            result.Caller.Method.Should().Be("LdrpLoadResourceFromAlternativeModule");
            result.Caller.Offset.Should().Be(0x2a2);

            result.Callee.Module.Should().Be("ntdll");
            result.Callee.Method.Should().Be("_security_check_cookie");
            result.Callee.Offset.Should().Be(0);
        }

        [Fact]
        public void Extract_Native_No_Callee()
        {
            // arrange
            var frameText = @"000000056e7cc800 00007ffc330dcc3a ntdll!LdrpLoadResourceFromAlternativeModule+0x2a2";
            var processor = new EeStackReportFactory();

            // act
            var result = processor.ExtractFrame(frameText);

            // assert
            result.ChildStackPointer.Should().Be(0x000000056e7cc800);
            result.ReturnAddress.Should().Be(0x00007ffc330dcc3a);
            result.Caller.Module.Should().Be("ntdll");
            result.Caller.Method.Should().Be("LdrpLoadResourceFromAlternativeModule");
            result.Caller.Offset.Should().Be(0x2a2);

            result.Callee.Should().BeNull();
        }

        [Fact]
        public void Extract_Managed_With_No_Callee()
        {
            // arrange
            var frameText = @"000000056e7ce810 00007ffbc079101f (MethodDesc 00007ffbc06887d0 +0x14f DomainBoundILStubClass.IL_STUB_PInvoke(IntPtr, Int32, IntPtr, _MINIDUMP_TYPE, MINIDUMP_EXCEPTION_INFORMATION ByRef, IntPtr, IntPtr))";
            var processor = new EeStackReportFactory();

            // act
            var result = processor.ExtractFrame(frameText);
            var codeLocation = result.Caller as ManagedCodeLocation;

            // assert
            codeLocation.Should().NotBeNull();
            result.ChildStackPointer.Should().Be(0x000000056e7ce810);
            result.ReturnAddress.Should().Be(0x00007ffbc079101f);
            result.Caller.Module.Should().BeNull();
            result.Caller.Method.Should().Be("DomainBoundILStubClass.IL_STUB_PInvoke(IntPtr, Int32, IntPtr, _MINIDUMP_TYPE, MINIDUMP_EXCEPTION_INFORMATION ByRef, IntPtr, IntPtr)");
            result.Caller.Offset.Should().Be(0x14f);
            codeLocation.MethodDescriptor.Should().Be(0x00007ffbc06887d0);

            result.Callee.Should().BeNull();
        }

        [Fact]
        public void Extract_Managed_Calling_Managed()
        {
            // arrange
            var frameText = @"000000e49b0fedc0 00007ffbc0790a33 (MethodDesc 00007ffbc06873b0 +0x293 Triage.Mortician.IntegrationTest.DumpHelper.CreateDump(System.String)), calling 00007ffbc0790180 (stub for Triage.Mortician.IntegrationTest.DumpHelper.MiniDumpWriteDump(IntPtr, Int32, IntPtr, _MINIDUMP_TYPE, MINIDUMP_EXCEPTION_INFORMATION ByRef, IntPtr, IntPtr))";
            var processor = new EeStackReportFactory();

            // act
            var result = processor.ExtractFrame(frameText);
            var callerLocation = result.Caller as ManagedCodeLocation;
            var calleeLocation = result.Callee as ManagedCodeLocation;

            // assert
            callerLocation.Should().NotBeNull();
            calleeLocation.Should().NotBeNull();
            result.ChildStackPointer.Should().Be(0x000000e49b0fedc0);
            result.ReturnAddress.Should().Be(0x00007ffbc0790a33);

        }

        #region Sample !eestack output
        private const string HELLO_WORLD = @"---------------------------------------------
Thread   0
Current frame: ntdll!NtGetContextThread+0x14
Child-SP         RetAddr          Caller, Callee
000000056e7cc800 00007ffc330dcc3a ntdll!LdrpLoadResourceFromAlternativeModule+0x2a2, calling ntdll!_security_check_cookie
000000056e7cc830 00007ffc330d4af3 ntdll!LdrpSetAlternateResourceModuleHandle+0x46b, calling ntdll!RtlReleaseSRWLockExclusive
000000056e7cc890 00007ffc330dc6ad ntdll!LdrpGetRcConfig+0x69, calling ntdll!_security_check_cookie
000000056e7cc8e0 00007ffc330c8c40 ntdll!LdrpGetFileSizeFromLoadAsDataTable+0x68, calling ntdll!RtlLeaveCriticalSection
000000056e7cc920 00007ffc330ba095 ntdll!LdrpGetImageSize+0x81, calling ntdll!NtQueryVirtualMemory
000000056e7cc990 00007ffc330ba200 ntdll!LdrpAccessResourceDataNoMultipleLanguage+0x11c, calling ntdll!RtlImageRvaToSection
000000056e7cca50 00007ffc331010e1 ntdll!LdrpResCompareResourceNames+0xb9, calling ntdll!_security_check_cookie
000000056e7cca70 00007ffc330c265b ntdll!RtlpAllocateHeapInternal+0xeb, calling ntdll!RtlpLowFragHeapAllocFromContext
000000056e7ccb50 00007ffc330e5f43 ntdll!RtlpDosPathNameToRelativeNtPathName+0x2e3, calling ntdll!_security_check_cookie
000000056e7ccbe0 00007ffc330b9d6e ntdll!LdrpGetFromMUIMemCache+0x16a, calling ntdll!RtlReleaseSRWLockShared
000000056e7ccc50 00007ffc331019ed ntdll!LdrResGetRCConfig+0x12d, calling ntdll!_security_check_cookie
000000056e7ccc90 00007ffc330dd969 ntdll!GetLCIDFromLangListNodeWithLICCheck+0xf9, calling ntdll!_security_check_cookie
000000056e7ccce0 00007ffc33101015 ntdll!LdrpResSearchResourceInsideDirectory+0x1031, calling ntdll!_security_check_cookie
000000056e7cce10 00007ffc2f48dcfa ucrtbase!__crt_stdio_output::output_processor<wchar_t,__crt_stdio_output::console_output_adapter<wchar_t>,__crt_stdio_output::format_validation_base<wchar_t,__crt_stdio_output::console_output_adapter<wchar_t> > >::type_case_s+0x5e, calling ucrtbase!wcsnlen
000000056e7cce40 00007ffc2f48e19d ucrtbase!__crt_stdio_output::output_processor<wchar_t,__crt_stdio_output::string_output_adapter<wchar_t>,__crt_stdio_output::format_validation_base<wchar_t,__crt_stdio_output::string_output_adapter<wchar_t> > >::state_case_type+0x1dd, calling ucrtbase!_security_check_cookie
000000056e7cceb0 00007ffc2f48e5f6 ucrtbase!__crt_stdio_output::output_processor<wchar_t,__crt_stdio_output::string_output_adapter<wchar_t>,__crt_stdio_output::format_validation_base<wchar_t,__crt_stdio_output::string_output_adapter<wchar_t> > >::process+0x236, calling ucrtbase!__crt_stdio_output::output_processor<wchar_t,__crt_stdio_output::string_output_adapter<wchar_t>,__crt_stdio_output::format_validation_base<wchar_t,__crt_stdio_output::string_output_adapter<wchar_t> > >::state_case_type
000000056e7ccf40 00007ffc2f490002 ucrtbase!common_vsprintf<__crt_stdio_output::format_validation_base,wchar_t>+0x1a2, calling ucrtbase!_security_check_cookie
000000056e7cd050 00007ffc330e5f43 ntdll!RtlpDosPathNameToRelativeNtPathName+0x2e3, calling ntdll!_security_check_cookie
000000056e7cd0e0 00007ffc2f8d1984 KERNELBASE!ConstructKernelKeyPath+0xa4, calling ntdll!NtQueryKey
000000056e7cd150 00007ffc2f8d16b0 KERNELBASE!LocalBaseRegOpenKey+0x240, calling KERNELBASE!_security_check_cookie
000000056e7cd280 00007ffc330c265b ntdll!RtlpAllocateHeapInternal+0xeb, calling ntdll!RtlpLowFragHeapAllocFromContext
000000056e7cd320 00007ffc2f8d1242 KERNELBASE!LocalBaseRegQueryValue+0x282, calling KERNELBASE!_security_check_cookie
000000056e7cd330 00007ffc2f8d789e KERNELBASE!LocalFree+0x2e, calling ntdll!RtlFreeHeap
000000056e7cd340 00007ffc330ecd47 ntdll!RtlEqualSid+0x27, calling ntdll!memcmp
000000056e7cd350 00007ffc330d6439 ntdll!SbSelectProcedure+0x1a9, calling ntdll!_security_check_cookie
000000056e7cd4b0 00007ffc2f8e8238 KERNELBASE!SetFilePointer+0xd8, calling KERNELBASE!_security_check_cookie
000000056e7cd4c0 00007ffc2f8d25f9 KERNELBASE!WriteFile+0x79, calling ntdll!NtWriteFile
000000056e7cd510 00007ffc330c8a33 ntdll!LdrpFindLoadedDllByHandle+0xc7, calling ntdll!RtlReleaseSRWLockExclusive
000000056e7cd520 00007ffc330ef924 ntdll!LdrpDecrementModuleLoadCountEx+0x50, calling ntdll!RtlReleaseSRWLockExclusive
000000056e7cd550 00007ffc330ef881 ntdll!LdrUnloadDll+0x51, calling ntdll!LdrpDereferenceModule
000000056e7cd570 00007ffc26e8184e dbgcore!WriteAtOffset+0x82, calling dbgcore!guard_dispatch_icall_nop
000000056e7cd580 00007ffc2f8bbced KERNELBASE!FreeLibrary+0x1d, calling ntdll!LdrUnloadDll
000000056e7cd5b0 00007ffc26e8386b dbgcore!WriteMiscInfo+0x2eb, calling dbgcore!_security_check_cookie
000000056e7cda00 00007ffc26e975af dbgcore!NtWin32LiveSystemProvider::QuerySystemMemoryInformation+0x23f, calling dbgcore!_security_check_cookie
000000056e7cdac0 00007ffc2f8e8238 KERNELBASE!SetFilePointer+0xd8, calling KERNELBASE!_security_check_cookie
000000056e7cdad0 00007ffc2f8d25f9 KERNELBASE!WriteFile+0x79, calling ntdll!NtWriteFile
000000056e7cdb40 00007ffc26e8ac9e dbgcore!Win32FileOutputProvider::WriteAll+0x1e, calling KERNELBASE!WriteFile
000000056e7cdb80 00007ffc26e8184e dbgcore!WriteAtOffset+0x82, calling dbgcore!guard_dispatch_icall_nop
000000056e7cdbc0 00007ffc26e82b96 dbgcore!WriteSystemMemoryInformation+0xb2, calling dbgcore!_security_check_cookie
000000056e7cdbf0 00007ffc2f8d25f9 KERNELBASE!WriteFile+0x79, calling ntdll!NtWriteFile
000000056e7cdc10 00007ffc2f8e8238 KERNELBASE!SetFilePointer+0xd8, calling KERNELBASE!_security_check_cookie
000000056e7cdc20 00007ffc2f8d25f9 KERNELBASE!WriteFile+0x79, calling ntdll!NtWriteFile
000000056e7cdc30 00007ffc26e977c9 dbgcore!NtWin32LiveSystemProvider::QueryProcessVmCounters+0x1f9, calling dbgcore!_security_check_cookie
000000056e7cdc90 00007ffc26e8ac9e dbgcore!Win32FileOutputProvider::WriteAll+0x1e, calling KERNELBASE!WriteFile
000000056e7cdcd0 00007ffc26e8184e dbgcore!WriteAtOffset+0x82, calling dbgcore!guard_dispatch_icall_nop
000000056e7cdd10 00007ffc26e82c61 dbgcore!WriteProcessVmCounters+0xb5, calling dbgcore!_security_check_cookie
000000056e7cddd0 00007ffc26e84642 dbgcore!WriteFullMemory+0x22, calling dbgcore!_chkstk
000000056e7cde10 00007ffc26e8514a dbgcore!WriteDumpData+0x31a, calling dbgcore!WriteFullMemory
000000056e7cdec0 00007ffc26e86096 dbgcore!MiniDumpProvideDump+0x59a, calling dbgcore!WriteDumpData
000000056e7cdfc0 00007ffc1fd855b9 clr!EEHeapFreeInProcessHeap+0x45, calling kernel32!HeapFreeStub
000000056e7ce100 00007ffc1fda7c08 clr!MethodDesc::GetOrCreatePrecode+0x6c, calling clr!_security_check_cookie
000000056e7ce110 00007ffc330c5c79 ntdll!RtlpAllocateHeap+0xa79, calling ntdll!memset
000000056e7ce180 00007ffc26e8666c dbgcore!DetermineOutputProvider+0xf0, calling dbgcore!guard_dispatch_icall_nop
000000056e7ce2f0 00007ffc1fd929ae clr!Precode::SetTargetInterlocked+0x2d2, calling kernel32!FlushInstructionCacheStub
000000056e7ce370 00007ffc330c2b55 ntdll!RtlpAllocateHeapInternal+0x5e5, calling ntdll!RtlpAllocateHeap
000000056e7ce3d0 00007ffc330c5c79 ntdll!RtlpAllocateHeap+0xa79, calling ntdll!memset
000000056e7ce3e0 00007ffc3310a35c ntdll!RtlpInitializeHeapSegment+0x190, calling ntdll!RtlpInsertFreeBlock
000000056e7ce400 00007ffc33109673 ntdll!RtlpPopulateListIndex+0x9f, calling ntdll!RtlpHeapAddListEntry
000000056e7ce450 00007ffc33109185 ntdll!RtlCreateHeap+0x8e5, calling ntdll!_security_check_cookie
000000056e7ce540 00007ffc1fd919d1 clr!PreStubWorker+0x462, calling kernel32!SetLastErrorStub
000000056e7ce710 00007ffc26e86937 dbgcore!MiniDumpWriteDump+0x267, calling dbgcore!MiniDumpProvideDump
000000056e7ce810 00007ffbc079101f (MethodDesc 00007ffbc06887d0 +0x14f DomainBoundILStubClass.IL_STUB_PInvoke(IntPtr, Int32, IntPtr, _MINIDUMP_TYPE, MINIDUMP_EXCEPTION_INFORMATION ByRef, IntPtr, IntPtr))
000000056e7ce888 00007ffbc079101f (MethodDesc 00007ffbc06887d0 +0x14f DomainBoundILStubClass.IL_STUB_PInvoke(IntPtr, Int32, IntPtr, _MINIDUMP_TYPE, MINIDUMP_EXCEPTION_INFORMATION ByRef, IntPtr, IntPtr))
000000056e7ce940 00007ffbc0790863 (MethodDesc 00007ffbc0687268 +0x293 Triage.Mortician.IntegrationTest.DumpHelper.CreateDump(System.String)), calling 00007ffbc0790140 (stub for Triage.Mortician.IntegrationTest.DumpHelper.MiniDumpWriteDump(IntPtr, Int32, IntPtr, _MINIDUMP_TYPE, MINIDUMP_EXCEPTION_INFORMATION ByRef, IntPtr, IntPtr))
000000056e7cea80 00007ffbc079054e (MethodDesc 00007ffbc0685a50 +0xce Triage.TestApplications.Console.Program.Main(System.String[])), calling 00007ffbc07900f0 (stub for Triage.Mortician.IntegrationTest.DumpHelper.CreateDump(System.String))
000000056e7ceb00 00007ffc1fd86bb3 clr!CallDescrWorkerInternal+0x83
000000056e7ceb40 00007ffc1fd86a70 clr!CallDescrWorkerWithHandler+0x4e, calling clr!CallDescrWorkerInternal
000000056e7ceb50 00007ffc1fe4d5b2 clr!ArgIteratorTemplate<ArgIteratorBase>::GetNextOffset+0xda, calling clr!MetaSig::GetElemSize
000000056e7ceb80 00007ffc1fd8735d clr!MethodDescCallSite::CallTargetWorker+0xf8, calling clr!CallDescrWorkerWithHandler
000000056e7cebd0 00007ffc1fddf011 clr!MethodDesc::IsVoid+0x21, calling clr!MetaSig::IsReturnTypeVoid
000000056e7cec80 00007ffc1fddec1c clr!RunMain+0x1e7, calling clr!MethodDescCallSite::CallTargetWorker
000000056e7cecf0 00007ffc2f8fd1d3 KERNELBASE!QuirkIsEnabled3+0x23, calling kernel32!QuirkIsEnabled3Worker
000000056e7cee20 00007ffc1fe343c8 clr!Thread::SetBackground+0x9f, calling clr!ThreadSuspend::UnlockThreadStore
000000056e7cee60 00007ffc1fddee06 clr!Assembly::ExecuteMainMethod+0xb6, calling clr!RunMain
000000056e7cef10 00007ffc330c265b ntdll!RtlpAllocateHeapInternal+0xeb, calling ntdll!RtlpLowFragHeapAllocFromContext
000000056e7cef50 00007ffc330c0428 ntdll!RtlFreeHeap+0x208, calling ntdll!RtlpHpStackLoggingEnabled
000000056e7cef70 00007ffc1fd855b9 clr!EEHeapFreeInProcessHeap+0x45, calling kernel32!HeapFreeStub
000000056e7cefa0 00007ffc1fe5595e clr!SString::~SString+0x3e
000000056e7cefd0 00007ffc330c0428 ntdll!RtlFreeHeap+0x208, calling ntdll!RtlpHpStackLoggingEnabled
000000056e7ceff0 00007ffc1fd855b9 clr!EEHeapFreeInProcessHeap+0x45, calling kernel32!HeapFreeStub
000000056e7cf010 00007ffc330c0428 ntdll!RtlFreeHeap+0x208, calling ntdll!RtlpHpStackLoggingEnabled
000000056e7cf070 00007ffc1fd855b9 clr!EEHeapFreeInProcessHeap+0x45, calling kernel32!HeapFreeStub
000000056e7cf0c0 00007ffc1fe814a0 clr!CLRConfig::GetConfigValue+0x14, calling clr!CLRConfig::GetConfigValue
000000056e7cf0e0 00007ffc1fd85609 clr!operator delete+0x29
000000056e7cf0f0 00007ffc1fecbd67 clr!MulticoreJitManager::AutoStartProfile+0x60, calling clr!Wrapper<ETW::CEtwTracer * __ptr64,&DoNothing<ETW::CEtwTracer * __ptr64>,&Delete<ETW::CEtwTracer>,0,&CompareDefault<ETW::CEtwTracer * __ptr64>,2,1>::~Wrapper<ETW::CEtwTracer * __ptr64,&DoNothing<ETW::CEtwTracer * __ptr64>,&Delete<ETW::CEtwTracer>,0,&CompareDefault<ETW::CEtwTracer * __ptr64>,2,1>
000000056e7cf110 00007ffc1fe556de clr!Wrapper<IAssemblyName * __ptr64,&DoNothing<IAssemblyName * __ptr64>,&DoTheRelease<IAssemblyName>,0,&CompareDefault<IAssemblyName * __ptr64>,2,1>::~Wrapper<IAssemblyName * __ptr64,&DoNothing<IAssemblyName * __ptr64>,&DoTheRelease<IAssemblyName>,0,&CompareDefault<IAssemblyName * __ptr64>,2,1>+0x52
000000056e7cf120 00007ffc1ff16f39 clr!GCPreemp::GCPreemp+0xe, calling clr!GetThread
000000056e7cf150 00007ffc1fddecfb clr!SystemDomain::ExecuteMainMethod+0x57c, calling clr!Assembly::ExecuteMainMethod
000000056e7cf3e0 00007ffc1fd8ff9c clr!AllocateObject+0x10a
000000056e7cf410 00007ffc1fd8bbcc clr!HndCreateHandle+0xe0, calling clr!StressLog::LogOn
000000056e7cf460 00007ffc1fe347f9 clr!CreateGlobalHandle+0xe, calling clr!GetCurrentThreadHomeHeapNumber
000000056e7cf490 00007ffc1fecdbb3 clr!AppDomain::SetupSharedStatics+0xf0, calling clr!ErectWriteBarrier
000000056e7cf4b0 00007ffc1fe0ca29 clr!SafeHandle::Init+0x41, calling clr!MethodDesc::GetSlot
000000056e7cf4e0 00007ffc1fe12acd clr!EEStartupHelper+0x795, calling clr!ConfigDWORD::val
000000056e7cf700 00007ffc1fe12319 clr!EEStartup+0x15, calling clr!EEStartupHelper
000000056e7cf760 00007ffc1fddeaf4 clr!ExecuteEXE+0x3f, calling clr!SystemDomain::ExecuteMainMethod
000000056e7cf7d0 00007ffc1fddea72 clr!_CorExeMainInternal+0xb2, calling clr!ExecuteEXE
000000056e7cf830 00007ffc330f70d0 ntdll!RtlSetLastWin32Error+0x40, calling ntdll!_security_check_cookie
000000056e7cf860 00007ffc1fddef34 clr!CorExeMain+0x14, calling clr!_CorExeMainInternal
000000056e7cf8a0 00007ffc21437b2d mscoreei!CorExeMain+0x112
000000056e7cf8d0 00007ffc23641504 mscoree!GetShimImpl+0x18, calling mscoree!InitShimImpl
000000056e7cf8e0 00007ffc2364a54f mscoree!CorExeMain_Exported+0xef, calling kernel32!GetProcAddressStub
000000056e7cf900 00007ffc2364a4cc mscoree!CorExeMain_Exported+0x6c, calling mscoree!guard_dispatch_icall_nop
000000056e7cf930 00007ffc31223034 kernel32!BaseThreadInitThunk+0x14, calling kernel32!guard_dispatch_icall_nop
000000056e7cf960 00007ffc33121461 ntdll!RtlUserThreadStart+0x21, calling ntdll!guard_dispatch_icall_nop
---------------------------------------------
Thread   6
Current frame: ntdll!NtWaitForSingleObject+0x14
Child-SP         RetAddr          Caller, Callee
000000056efff5a0 00007ffc2f8d9252 KERNELBASE!WaitForSingleObjectEx+0xa2, calling ntdll!NtWaitForSingleObject
000000056efff640 00007ffc1fe428b7 clr!CLREventWaitHelper2+0x3c, calling kernel32!WaitForSingleObjectEx
000000056efff650 00007ffc30cc66ad combase!RoInitialize+0xd, calling combase!CoVrfDllMainCheck
000000056efff680 00007ffc1fe4286f clr!CLREventWaitHelper+0x1f, calling clr!CLREventWaitHelper2
000000056efff6a0 00007ffc2f8d9252 KERNELBASE!WaitForSingleObjectEx+0xa2, calling ntdll!NtWaitForSingleObject
000000056efff6b0 00007ffc330f6129 ntdll!EtwEventWrite+0x29, calling ntdll!EtwpEventWriteFull
000000056efff6e0 00007ffc1fe427f8 clr!CLREventBase::WaitEx+0x7c, calling clr!CLREventWaitHelper
000000056efff720 00007ffc1ff13db3 clr!Thread::DoExtraWorkForFinalizer+0x123, calling clr!_security_check_cookie
000000056efff740 00007ffc1fe2cdc5 clr!FinalizerThread::ProcessProfilerAttachIfNecessary+0x60, calling kernel32!WaitForSingleObject
000000056efff750 00007ffc2f8f3c1c KERNELBASE!SetEvent+0xc, calling ntdll!NtSetEvent
000000056efff770 00007ffc1fe429b8 clr!FinalizerThread::WaitForFinalizerEvent+0x44, calling clr!CLREventBase::WaitEx
000000056efff7b0 00007ffc1ff13b84 clr!FinalizerThread::FinalizerThreadWorker+0x54, calling clr!FinalizerThread::WaitForFinalizerEvent
000000056efff7e0 00007ffc1fd851a1 clr!ClrFlsIncrementValue+0x29
000000056efff7f0 00007ffc1fd87b21 clr!ManagedThreadBase_DispatchInner+0x39
000000056efff830 00007ffc1fd87a90 clr!ManagedThreadBase_DispatchMiddle+0x6c, calling clr!ManagedThreadBase_DispatchInner
000000056efff840 00007ffc1fd854a7 clr!ThreadSuspend::UnlockThreadStore+0x53, calling clr!ClrFlsIncrementValue
000000056efff860 00007ffc1fd85e12 clr!REGUTIL::GetConfigInteger+0x62, calling clr!REGUTIL::RegCacheValueNameSeenPerhaps
000000056efff8e0 00007ffc1fd85eaa clr!EEConfig::GetConfiguration_DontUse_+0x36, calling clr!GetThread
000000056efff900 00007ffc1fd87651 clr!FrameWithCookie<DebuggerU2MCatchHandlerFrame>::FrameWithCookie<DebuggerU2MCatchHandlerFrame>+0x26, calling clr!Frame::Push
000000056efff930 00007ffc1fd879cd clr!ManagedThreadBase_DispatchOuter+0x75, calling clr!ManagedThreadBase_DispatchMiddle
000000056efff940 00007ffc1fe101af clr!EEConfig::GetConfigDWORD_DontUse_+0x3b, calling clr!EEConfig::GetConfiguration_DontUse_
000000056efff9c0 00007ffc1fe074fa clr!FinalizerThread::FinalizerThreadStart+0x10a, calling clr!ManagedThreadBase_DispatchOuter
000000056efffa00 00007ffc1fd855b9 clr!EEHeapFreeInProcessHeap+0x45, calling kernel32!HeapFreeStub
000000056efffa60 00007ffc1fe32e8f clr!Thread::intermediateThreadProc+0x86
000000056efffae0 00007ffc1fe32e6f clr!Thread::intermediateThreadProc+0x66, calling clr!_chkstk
000000056efffb20 00007ffc31223034 kernel32!BaseThreadInitThunk+0x14, calling kernel32!guard_dispatch_icall_nop
000000056efffb50 00007ffc33121461 ntdll!RtlUserThreadStart+0x21, calling ntdll!guard_dispatch_icall_nop
";

        private const string IIS_EXAMPLE = @"---------------------------------------------
Thread   6
Current frame: ntdll!NtRemoveIoCompletion+0x14
Child-SP         RetAddr          Caller, Callee
000000f66b77fa40 00007fffb53c9a8f KERNELBASE!GetQueuedCompletionStatus+0x3f, calling ntdll!NtRemoveIoCompletion
000000f66b77fa60 00007fff94c14e42 *** ERROR: Symbol file could not be found.  Defaulted to export symbols for w3dt.dll - 
w3dt!HTTP_WRAPPER::QueryState+0x3df2, calling ntdll!LdrpDispatchUserCallTarget
000000f66b77faa0 00007fff9d962b46 *** ERROR: Symbol file could not be found.  Defaulted to export symbols for w3tp.dll - 
w3tp!THREAD_POOL::PostCompletion+0x86, calling kernel32!GetQueuedCompletionStatusStub
000000f66b77fb00 00007fff9d961819 w3tp+0x1819, calling ntdll!LdrpDispatchUserCallTarget
000000f66b77fb40 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66b77fb70 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
---------------------------------------------
Thread  38
Current frame: ntdll!NtWaitForMultipleObjects+0x14
Child-SP         RetAddr          Caller, Callee
000000f66c7ff1d0 00007fffb53bdcb0 KERNELBASE!WaitForMultipleObjectsEx+0xf0, calling ntdll!NtWaitForMultipleObjects
000000f66c7ff220 00007fffb816b596 ntdll!EtwpEventWriteFull+0x176, calling ntdll!_security_check_cookie
000000f66c7ff230 00007fffb816b596 ntdll!EtwpEventWriteFull+0x176, calling ntdll!_security_check_cookie
000000f66c7ff2b0 00007fffb539d4fb KERNELBASE!NlsValidateLocale+0xbb, calling KERNELBASE!_security_check_cookie
000000f66c7ff300 00007fffb53a995f KERNELBASE!WaitForSingleObjectEx+0x9f, calling ntdll!NtWaitForSingleObject
000000f66c7ff380 00007fffa31ccceb clr!Thread::InternalReset+0x10c, calling clr!Thread::SetBackground
000000f66c7ff3a0 00007fffa32b7a1f clr!CLREventWaitHelper2+0x3c, calling kernel32!WaitForSingleObjectEx
000000f66c7ff3e0 00007fffa32b79d7 clr!CLREventWaitHelper+0x1f, calling clr!CLREventWaitHelper2
000000f66c7ff400 00007fffb816b419 ntdll!EtwEventWrite+0x29, calling ntdll!EtwpEventWriteFull
000000f66c7ff410 00007fffa3308314 clr!RCWCleanupList::CleanupAllWrappers+0x13ad04, calling clr!RCWCleanupList::ReleaseRCWListInCorrectCtx
000000f66c7ff440 00007fffa32b7958 clr!CLREventBase::WaitEx+0x7c, calling clr!CLREventWaitHelper
000000f66c7ff450 00007fffa35f409a clr!CoTemplate_qh+0x7a, calling clr!_security_check_cookie
000000f66c7ff460 00007fffa355dfad clr!CoTemplate_h+0x65, calling clr!_security_check_cookie
000000f66c7ff4d0 00007fffa32b7b8a clr!FinalizerThread::WaitForFinalizerEvent+0xb6, calling kernel32!WaitForMultipleObjectsEx
000000f66c7ff510 00007fffa31ca9e4 clr!FinalizerThread::FinalizerThreadWorker+0x54, calling clr!FinalizerThread::WaitForFinalizerEvent
000000f66c7ff550 00007fffa3147ce5 clr!ManagedThreadBase_DispatchInner+0x39
000000f66c7ff590 00007fffa3147c60 clr!ManagedThreadBase_DispatchMiddle+0x6c, calling clr!ManagedThreadBase_DispatchInner
000000f66c7ff640 00007fffa322db72 clr!Wrapper<void * __ptr64,&DoNothing<void * __ptr64>,&VoidCloseHandle,-1,&CompareDefault<void * __ptr64>,2,1>::~Wrapper<void * __ptr64,&DoNothing<void * __ptr64>,&VoidCloseHandle,-1,&CompareDefault<void * __ptr64>,2,1>+0x30, calling kernel32!CloseHandle
000000f66c7ff660 00007fffa31473bd clr!FrameWithCookie<DebuggerU2MCatchHandlerFrame>::FrameWithCookie<DebuggerU2MCatchHandlerFrame>+0x26, calling clr!Frame::Push
000000f66c7ff680 00007fffa325dfbe clr!ProfilingAPIAttachDetach::CreateAttachThread+0x76, calling clr!Wrapper<void * __ptr64,&DoNothing<void * __ptr64>,&VoidCloseHandle,-1,&CompareDefault<void * __ptr64>,2,1>::~Wrapper<void * __ptr64,&DoNothing<void * __ptr64>,&VoidCloseHandle,-1,&CompareDefault<void * __ptr64>,2,1>
000000f66c7ff690 00007fffa3147b9e clr!ManagedThreadBase_DispatchOuter+0x75, calling clr!ManagedThreadBase_DispatchMiddle
000000f66c7ff6a0 00007fffa323f217 clr!EEConfig::GetConfigDWORD_DontUse_+0x3b, calling clr!EEConfig::GetConfiguration_DontUse_
000000f66c7ff6d0 00007fffb816a670 ntdll!RtlSetLastWin32Error+0x40, calling ntdll!_security_check_cookie
000000f66c7ff720 00007fffa323182a clr!FinalizerThread::FinalizerThreadStart+0x10a, calling clr!ManagedThreadBase_DispatchOuter
000000f66c7ff760 00007fffa3146969 clr!EEHeapFreeInProcessHeap+0x45, calling kernel32!HeapFreeStub
000000f66c7ff7c0 00007fffa330159f clr!Thread::intermediateThreadProc+0x86
000000f66c7ff840 00007fffa330157f clr!Thread::intermediateThreadProc+0x66, calling clr!_chkstk
000000f66c7ff880 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66c7ff8b0 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
---------------------------------------------
Thread  40
Current frame: ntdll!NtDelayExecution+0x14
Child-SP         RetAddr          Caller, Callee
000000f66c8bf710 00007fffb53c7217 KERNELBASE!SleepEx+0xa7, calling ntdll!NtDelayExecution
000000f66c8bf780 00007fffb53c728c KERNELBASE!SleepEx+0x11c, calling ntdll!RtlActivateActivationContextUnsafeFast
000000f66c8bf7b0 00007fffa3149bcd clr!ThreadpoolMgr::TimerThreadFire+0x49, calling kernel32!SleepEx
000000f66c8bf850 00007fffa3149b7f clr!ThreadpoolMgr::TimerThreadStart+0x6f, calling clr!ThreadpoolMgr::TimerThreadFire
000000f66c8bf880 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66c8bf8b0 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
---------------------------------------------
Thread  41
Current frame: ntdll!NtWaitForSingleObject+0x14
Child-SP         RetAddr          Caller, Callee
000000f66c93faf0 00007fffb53a995f KERNELBASE!WaitForSingleObjectEx+0x9f, calling ntdll!NtWaitForSingleObject
000000f66c93fb20 00007fffa37b46db clr!DebuggerController::EnableTraceCall+0x6f, calling clr!EEJitManager::CodeHeapIterator::~CodeHeapIterator
000000f66c93fb90 00007fffa32b7a1f clr!CLREventWaitHelper2+0x3c, calling kernel32!WaitForSingleObjectEx
000000f66c93fbd0 00007fffa32b79d7 clr!CLREventWaitHelper+0x1f, calling clr!CLREventWaitHelper2
000000f66c93fbf0 00007fffa3146071 clr!ClrFlsIncrementValue+0x29
000000f66c93fc20 00007fffa3146071 clr!ClrFlsIncrementValue+0x29
000000f66c93fc30 00007fffa32b7958 clr!CLREventBase::WaitEx+0x7c, calling clr!CLREventWaitHelper
000000f66c93fc50 00007fffa3146497 clr!ThreadSuspend::UnlockThreadStore+0x53, calling clr!ClrFlsIncrementValue
000000f66c93fc80 00007fffa32b3f70 clr!Thread::SetBackground+0x9f, calling clr!ThreadSuspend::UnlockThreadStore
000000f66c93fcc0 00007fffa3207e1c clr!AppDomain::ADUnloadThreadStart+0x18c, calling clr!CLREventBase::WaitEx
000000f66c93fdf0 00007fffa330159f clr!Thread::intermediateThreadProc+0x86
000000f66c93fef0 00007fffa330157f clr!Thread::intermediateThreadProc+0x66, calling clr!_chkstk
000000f66c93ff30 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66c93ff60 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
---------------------------------------------
Thread  42
Current frame: ntdll!NtWaitForSingleObject+0x14
Child-SP         RetAddr          Caller, Callee
000000f66c9bfa10 00007fffb53a995f KERNELBASE!WaitForSingleObjectEx+0x9f, calling ntdll!NtWaitForSingleObject
000000f66c9bfab0 00007fffa314862e clr!CLRSemaphore::Wait+0x8a, calling kernel32!WaitForSingleObjectEx
000000f66c9bfac0 00007fffa3147eab clr!SpinLock::Holder::Holder+0x20, calling clr!SpinLock::GetLock
000000f66c9bfb70 00007fffa314892b clr!ThreadpoolMgr::UnfairSemaphore::Wait+0x115, calling clr!CLRSemaphore::Wait
000000f66c9bfbc0 00007fffa314889a clr!ThreadpoolMgr::WorkerThreadStart+0x2bb, calling clr!ThreadpoolMgr::UnfairSemaphore::Wait
000000f66c9bfc60 00007fffa330159f clr!Thread::intermediateThreadProc+0x86
000000f66c9bfde0 00007fffa330157f clr!Thread::intermediateThreadProc+0x66, calling clr!_chkstk
000000f66c9bfe20 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66c9bfe50 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
---------------------------------------------
Thread  43
Current frame: ntdll!NtWaitForMultipleObjects+0x14
Child-SP         RetAddr          Caller, Callee
000000f66ca3f950 00007fffb53bdcb0 KERNELBASE!WaitForMultipleObjectsEx+0xf0, calling ntdll!NtWaitForMultipleObjects
000000f66ca3f9f0 00007fffb53bddaf KERNELBASE!WaitForMultipleObjectsEx+0x1ef, calling ntdll!RtlActivateActivationContextUnsafeFast
000000f66ca3fa00 00007fffa3146071 clr!ClrFlsIncrementValue+0x29
000000f66ca3fa10 00007fffa3146071 clr!ClrFlsIncrementValue+0x29
000000f66ca3fa20 00007fffa31460a1 clr!CrstBase::Leave+0x65, calling clr!ClrFlsIncrementValue
000000f66ca3fa30 00007fffa3146016 clr!CrstBase::Enter+0x6a, calling ntdll!RtlTryEnterCriticalSection
000000f66ca3fa40 00007fffa3146071 clr!ClrFlsIncrementValue+0x29
000000f66ca3fa70 00007fffa3146497 clr!ThreadSuspend::UnlockThreadStore+0x53, calling clr!ClrFlsIncrementValue
000000f66ca3fa80 00007fffa314619f clr!ClrFlsGetValue+0x23
000000f66ca3faa0 00007fffb8134777 ntdll!RtlDeactivateActivationContextUnsafeFast+0xc7, calling ntdll!_security_check_cookie
000000f66ca3fab0 00007fffa31c4fc6 clr!IsWaitSpecialThread+0xe, calling clr!ClrFlsGetValue
000000f66ca3fad0 00007fffb8132313 ntdll!RtlActivateActivationContextUnsafeFast+0x93, calling ntdll!_security_check_cookie
000000f66ca3fae0 00007fffa31c4f75 clr!SetupThread+0x214, calling clr!ETW::ThreadLog::FireThreadCreated
000000f66ca3fbb0 00007fffb53c72a4 KERNELBASE!SleepEx+0x134, calling ntdll!RtlDeactivateActivationContextUnsafeFast
000000f66ca3fc20 00007fffa321fe55 clr!ThreadpoolMgr::MinimumRemainingWait+0x1d, calling kernel32!GetTickCountKernel32
000000f66ca3fc50 00007fffa321fe24 clr!ThreadpoolMgr::WaitThreadStart+0xdd, calling kernel32!WaitForMultipleObjectsEx
000000f66ca3fcc0 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66ca3fcf0 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
---------------------------------------------
Thread  44
Current frame: ntdll!NtWaitForMultipleObjects+0x14
Child-SP         RetAddr          Caller, Callee
000000f66cabe180 00007fffb53bdcb0 KERNELBASE!WaitForMultipleObjectsEx+0xf0, calling ntdll!NtWaitForMultipleObjects
000000f66cabe1b0 00007fffa3146969 clr!EEHeapFreeInProcessHeap+0x45, calling kernel32!HeapFreeStub
000000f66cabe1e0 00007fffa3152471 clr!ListLockEntry::`scalar deleting destructor'+0xe1
000000f66cabe220 00007fffb53bddaf KERNELBASE!WaitForMultipleObjectsEx+0x1ef, calling ntdll!RtlActivateActivationContextUnsafeFast
000000f66cabe250 00007fffa31551dd clr!MethodDesc::MakeJitWorker+0x862, calling clr!CrstBase::Leave
000000f66cabe320 00007fffa314619f clr!ClrFlsGetValue+0x23
000000f66cabe340 00007fffb76b7af5 combase!CObjectContext::QIHelper+0x105, calling ntdll!LdrpDispatchUserCallTarget
000000f66cabe350 00007fffa3209e02 clr!IsDbgHelperSpecialThread+0xe, calling clr!ClrFlsGetValue
000000f66cabe370 00007fffa314b38c clr!SafeQueryInterface+0xe8
000000f66cabe380 00007fffa37ac127 clr!ThisIsHelperThreadWorker+0x23, calling clr!ThisIsTempHelperThread
000000f66cabe390 00007fffb8140f20 ntdll!RtlFreeHeap+0x150, calling ntdll!RtlGetCurrentServiceSessionId
000000f66cabe3b0 00007fffa32b403c clr!SafeReleasePreemp+0x84
000000f66cabe3d0 00007fffa315a953 clr!COR_ILMETHOD_DECODER::COR_ILMETHOD_DECODER+0x23d, calling clr!validateTokenSig
000000f66cabe410 00007fffa32b57e5 clr!GetCurrentThreadTypeNT5+0xc5, calling clr!SafeReleasePreemp
000000f66cabe480 00007fffa32e69ca clr!WaitForMultipleObjectsEx_SO_TOLERANT+0x62, calling kernel32!WaitForMultipleObjectsEx
000000f66cabe4b0 00007fffa314ba42 clr!Thread::GetFinalApartment+0x1a, calling clr!Thread::GetApartment
000000f66cabe4e0 00007fffa32e6864 clr!Thread::DoAppropriateWaitWorker+0x1e4, calling clr!WaitForMultipleObjectsEx_SO_TOLERANT
000000f66cabe5e0 00007fffa32e665d clr!Thread::DoAppropriateWait+0x7d, calling clr!Thread::DoAppropriateWaitWorker
000000f66cabe660 00007fffa32fec35 clr!WaitHandleNative::CorWaitOneNative+0x165, calling clr!Thread::DoAppropriateWait
000000f66cabe710 00007fffa32e9187 clr!JIT_GetSharedNonGCStaticBase_Helper+0xd7, calling clr!HelperMethodFrameRestoreState
000000f66cabe7a0 00007fff9efd656b (MethodDesc 00007fff9ebcb048 +0x1b System.Threading.WaitHandle.InternalWaitOne(System.Runtime.InteropServices.SafeHandle, Int64, Boolean, Boolean)), calling 00007fffa32fead0 (stub for System.Threading.WaitHandle.WaitOneNative(System.Runtime.InteropServices.SafeHandle, UInt32, Boolean, Boolean))
000000f66cabe838 00007fffa32feb57 clr!WaitHandleNative::CorWaitOneNative+0x87, calling clr!LazyMachStateCaptureState
000000f66cabe890 00007fff9efd656b (MethodDesc 00007fff9ebcb048 +0x1b System.Threading.WaitHandle.InternalWaitOne(System.Runtime.InteropServices.SafeHandle, Int64, Boolean, Boolean)), calling 00007fffa32fead0 (stub for System.Threading.WaitHandle.WaitOneNative(System.Runtime.InteropServices.SafeHandle, UInt32, Boolean, Boolean))
000000f66cabe8c0 00007fff9f881af9 (MethodDesc 00007fff9ebcb018 +0x49 System.Threading.WaitHandle.WaitOne(System.TimeSpan, Boolean)), calling (MethodDesc 00007fff9ebcb048 +0 System.Threading.WaitHandle.InternalWaitOne(System.Runtime.InteropServices.SafeHandle, Int64, Boolean, Boolean))
000000f66cabe8d0 00007fff43bde844 (MethodDesc 00007fff43e26e58 +0x84 Microsoft.ApplicationInsights.Channel.InMemoryTransmitter.DequeueAndSend(System.TimeSpan)), calling (MethodDesc 00007fff43e26e58 +0xfb Microsoft.ApplicationInsights.Channel.InMemoryTransmitter.DequeueAndSend(System.TimeSpan))
000000f66cabe900 00007fff9f881b42 (MethodDesc 00007fff9ebcb030 +0x12 System.Threading.WaitHandle.WaitOne(System.TimeSpan))
000000f66cabe930 00007fff43bde583 (MethodDesc 00007fff43e26e38 +0x63 Microsoft.ApplicationInsights.Channel.InMemoryTransmitter.Runner())
000000f66cabe980 00007fff9efed436 (MethodDesc 00007fff9ebc57d0 +0x46 System.Threading.Tasks.Task.Execute())
000000f66cabe9c0 00007fff9ef9ca72 (MethodDesc 00007fff9ebca660 +0x162 System.Threading.ExecutionContext.RunInternal(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean))
000000f66cabea90 00007fff9ef9c904 (MethodDesc 00007fff9ebca650 +0x14 System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean)), calling (MethodDesc 00007fff9ebca660 +0 System.Threading.ExecutionContext.RunInternal(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean))
000000f66cabeaa0 00007fff9ef6706a (MethodDesc 00007fff9ebc6a90 +0x1a System.MulticastDelegate.CtorOpened(System.Object, IntPtr, IntPtr)), calling clr!JIT_WriteBarrier
000000f66cabeac0 00007fff9efed6dc (MethodDesc 00007fff9ebc5850 +0x21c System.Threading.Tasks.Task.ExecuteWithThreadLocal(System.Threading.Tasks.Task ByRef)), calling (MethodDesc 00007fff9ebca650 +0 System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean))
000000f66cabeb70 00007fff9efecdf3 (MethodDesc 00007fff9ebc5840 +0x73 System.Threading.Tasks.Task.ExecuteEntry(Boolean)), calling (MethodDesc 00007fff9ebc5850 +0 System.Threading.Tasks.Task.ExecuteWithThreadLocal(System.Threading.Tasks.Task ByRef))
000000f66cabebb0 00007fff9ef9ca72 (MethodDesc 00007fff9ebca660 +0x162 System.Threading.ExecutionContext.RunInternal(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean))
000000f66cabec80 00007fff9ef9c904 (MethodDesc 00007fff9ebca650 +0x14 System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean)), calling (MethodDesc 00007fff9ebca660 +0 System.Threading.ExecutionContext.RunInternal(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean))
000000f66cabec90 00007fffa3144675 clr!ThePreStub+0x55, calling clr!PreStubWorker
000000f66cabecb0 00007fff9ef9c8c2 (MethodDesc 00007fff9ebca640 +0x52 System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object)), calling (MethodDesc 00007fff9ebca650 +0 System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean))
000000f66cabed00 00007fff9f97100c (MethodDesc 00007fff9ed18d10 +0x5c System.Threading.ThreadHelper.ThreadStart(System.Object)), calling (MethodDesc 00007fff9ebca640 +0 System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object))
000000f66cabed40 00007fffa3146793 clr!CallDescrWorkerInternal+0x83
000000f66cabed80 00007fffa3146665 clr!CallDescrWorkerWithHandler+0x4e, calling clr!CallDescrWorkerInternal
000000f66cabed90 00007fffa32a38af clr!ArgIteratorTemplate<ArgIteratorBase>::GetNextOffset+0xda, calling clr!MetaSig::GetElemSize
000000f66cabedc0 00007fffa314736d clr!MethodDescCallSite::CallTargetWorker+0xf8, calling clr!CallDescrWorkerWithHandler
000000f66cabeec0 00007fffa322bf59 clr!ThreadNative::KickOffThread_Worker+0x109, calling clr!MethodDescCallSite::CallTargetWorker
000000f66cabf0c0 00007fffb813b88a ntdll!RtlpAllocateHeapInternal+0xa0a, calling ntdll!RtlpAllocateHeap
000000f66cabf120 00007fffa3147ce5 clr!ManagedThreadBase_DispatchInner+0x39
000000f66cabf160 00007fffa3147c60 clr!ManagedThreadBase_DispatchMiddle+0x6c, calling clr!ManagedThreadBase_DispatchInner
000000f66cabf170 00007fffb813e5a1 ntdll!RtlpAllocateHeap+0xae1, calling ntdll!memset
000000f66cabf180 00007fffb53d7643 KERNELBASE!QuirkIsEnabled3+0x23, calling kernel32!QuirkIsEnabled3Worker
000000f66cabf1c0 00007fffa314a174 clr!Thread::SetLastThrownObject+0x35, calling clr!StressLog::LogOn
000000f66cabf230 00007fffa31473bd clr!FrameWithCookie<DebuggerU2MCatchHandlerFrame>::FrameWithCookie<DebuggerU2MCatchHandlerFrame>+0x26, calling clr!Frame::Push
000000f66cabf250 00007fffa31471cc clr!SystemDomain::GetAppDomainAtId+0x40, calling clr!AppDomain::CanThreadEnter
000000f66cabf260 00007fffa3147b9e clr!ManagedThreadBase_DispatchOuter+0x75, calling clr!ManagedThreadBase_DispatchMiddle
000000f66cabf280 00007fffa3146071 clr!ClrFlsIncrementValue+0x29
000000f66cabf2f0 00007fffa314a8c4 clr!ManagedThreadBase_DispatchInCorrectAD+0x15, calling clr!ManagedThreadBase_DispatchOuter
000000f66cabf320 00007fffa314a986 clr!Thread::DoADCallBack+0x278
000000f66cabf350 00007fffb766ed44 combase!ThreadFirstInitialize+0x78, calling combase!InitializeTlsApartmentAndEmptyContext
000000f66cabf360 00007fffb766fcc8 combase!RegisterThreadCleanupCallback+0x28, calling KERNELBASE!FlsSetValue
000000f66cabf3d0 00007fffb813af7e ntdll!RtlpAllocateHeapInternal+0xfe, calling ntdll!RtlpLowFragHeapAllocFromContext
000000f66cabf400 00007fffb766f44a combase!_CoInitializeEx+0x266, calling combase!__security_check_cookie
000000f66cabf4e0 00007fffa314a8db clr!ManagedThreadBase_DispatchInner+0x2c37, calling clr!Thread::DoADCallBack
000000f66cabf510 00007fffa3146071 clr!ClrFlsIncrementValue+0x29
000000f66cabf520 00007fffa3147c60 clr!ManagedThreadBase_DispatchMiddle+0x6c, calling clr!ManagedThreadBase_DispatchInner
000000f66cabf530 00007fffa3678852 clr!DebuggerController::ControllerLockHolder::ControllerLockHolder+0x22, calling clr!CrstBase::CrstHolder::CrstHolder
000000f66cabf540 00007fffa31460a1 clr!CrstBase::Leave+0x65, calling clr!ClrFlsIncrementValue
000000f66cabf570 00007fffa37b46db clr!DebuggerController::EnableTraceCall+0x6f, calling clr!EEJitManager::CodeHeapIterator::~CodeHeapIterator
000000f66cabf5f0 00007fffa31473bd clr!FrameWithCookie<DebuggerU2MCatchHandlerFrame>::FrameWithCookie<DebuggerU2MCatchHandlerFrame>+0x26, calling clr!Frame::Push
000000f66cabf620 00007fffa3147b9e clr!ManagedThreadBase_DispatchOuter+0x75, calling clr!ManagedThreadBase_DispatchMiddle
000000f66cabf6b0 00007fffa3147d1f clr!ManagedThreadBase_FullTransitionWithAD+0x2f, calling clr!ManagedThreadBase_DispatchOuter
000000f66cabf710 00007fffa322be3b clr!ThreadNative::KickOffThread+0xdb, calling clr!ManagedThreadBase_FullTransitionWithAD
000000f66cabf7b0 00007fffa31469b9 clr!operator delete+0x29
000000f66cabf7e0 00007fffa330159f clr!Thread::intermediateThreadProc+0x86
000000f66cabf9e0 00007fffa330157f clr!Thread::intermediateThreadProc+0x66, calling clr!_chkstk
000000f66cabfa20 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66cabfa50 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
---------------------------------------------
Thread  45
Current frame: ntdll!NtWaitForSingleObject+0x14
Child-SP         RetAddr          Caller, Callee
000000f66cb7f640 00007fffb53a995f KERNELBASE!WaitForSingleObjectEx+0x9f, calling ntdll!NtWaitForSingleObject
000000f66cb7f6e0 00007fffa314862e clr!CLRSemaphore::Wait+0x8a, calling kernel32!WaitForSingleObjectEx
000000f66cb7f6f0 00007fffa3147eab clr!SpinLock::Holder::Holder+0x20, calling clr!SpinLock::GetLock
000000f66cb7f7a0 00007fffa314892b clr!ThreadpoolMgr::UnfairSemaphore::Wait+0x115, calling clr!CLRSemaphore::Wait
000000f66cb7f7f0 00007fffa314889a clr!ThreadpoolMgr::WorkerThreadStart+0x2bb, calling clr!ThreadpoolMgr::UnfairSemaphore::Wait
000000f66cb7f890 00007fffa330159f clr!Thread::intermediateThreadProc+0x86
000000f66cb7fb10 00007fffa330157f clr!Thread::intermediateThreadProc+0x66, calling clr!_chkstk
000000f66cb7fb50 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66cb7fb80 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
---------------------------------------------
Thread  46
Current frame: ntdll!NtDelayExecution+0x14
Child-SP         RetAddr          Caller, Callee
000000f66cbfeae0 00007fffb53c7217 KERNELBASE!SleepEx+0xa7, calling ntdll!NtDelayExecution
000000f66cbfeb50 00007fffb53c728c KERNELBASE!SleepEx+0x11c, calling ntdll!RtlActivateActivationContextUnsafeFast
000000f66cbfeb80 00007fffa31462a3 clr!EESleepEx+0x33, calling kernel32!SleepEx
000000f66cbfebb0 00007fffa32a9284 clr!Thread::UserSleep+0xa5, calling clr!ClrSleepEx
000000f66cbfec10 00007fffa32a915d clr!ThreadNative::Sleep+0xad, calling clr!Thread::UserSleep
000000f66cbfec50 00007fff9f884f5f (MethodDesc 00007fff9ebae090 +0xff System.Diagnostics.Tracing.EventSource.WriteEventVarargs(Int32, System.Guid*, System.Object[])), calling (MethodDesc 00007fff9ed245b0 +0 System.Diagnostics.Tracing.EventSource.LogEventArgsMismatches(System.Reflection.ParameterInfo[], System.Object[]))
000000f66cbfed20 00007fff9efe896c (MethodDesc 00007fff9ebc3590 +0x4c System.DateTimeOffset.ValidateDate(System.DateTime, System.TimeSpan)), calling (MethodDesc 00007fff9ebc92c0 +0 System.DateTime..ctor(Int64, System.DateTimeKind))
000000f66cbfed30 00007fffa314625f clr!SystemNative::__GetSystemTimeAsFileTime+0xf, calling kernel32!GetSystemTimeAsFileTimeStub
000000f66cbfed48 00007fffa32a910f clr!ThreadNative::Sleep+0x5f, calling clr!LazyMachStateCaptureState
000000f66cbfed70 00007fff9efd6e8a (MethodDesc 00007fff9ebb39c8 +0xa System.Threading.Thread.Sleep(Int32)), calling 00007fffa32a90b0 (stub for System.Threading.Thread.SleepInternal(Int32))
000000f66cbfeda0 00007fff9f7d4786 (MethodDesc 00007fff9ebb39d8 +0x36 System.Threading.Thread.Sleep(System.TimeSpan)), calling (MethodDesc 00007fff9ebb39c8 +0 System.Threading.Thread.Sleep(Int32))
000000f66cbfede0 00007fff43bf784c (MethodDesc 00007fff43e25bc8 +0x2ac Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse.QuickPulseTelemetryModule.StateThreadWorker(System.Object)), calling (MethodDesc 00007fff9ebb39d8 +0 System.Threading.Thread.Sleep(System.TimeSpan))
000000f66cbfee10 00007fffa3144675 clr!ThePreStub+0x55, calling clr!PreStubWorker
000000f66cbfeec0 00007fff9ef9ca72 (MethodDesc 00007fff9ebca660 +0x162 System.Threading.ExecutionContext.RunInternal(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean))
000000f66cbfef90 00007fff9ef9c904 (MethodDesc 00007fff9ebca650 +0x14 System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean)), calling (MethodDesc 00007fff9ebca660 +0 System.Threading.ExecutionContext.RunInternal(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean))
000000f66cbfefc0 00007fff9ef9c8c2 (MethodDesc 00007fff9ebca640 +0x52 System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object)), calling (MethodDesc 00007fff9ebca650 +0 System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean))
000000f66cbff010 00007fff9f97100c (MethodDesc 00007fff9ed18d10 +0x5c System.Threading.ThreadHelper.ThreadStart(System.Object)), calling (MethodDesc 00007fff9ebca640 +0 System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object))
000000f66cbff020 00007fffa314754a clr!SigPointer::PeekElemTypeNormalized+0x32, calling clr!SigPointer::PeekElemTypeClosed
000000f66cbff050 00007fffa3146793 clr!CallDescrWorkerInternal+0x83
000000f66cbff090 00007fffa3146665 clr!CallDescrWorkerWithHandler+0x4e, calling clr!CallDescrWorkerInternal
000000f66cbff0a0 00007fffa32a38af clr!ArgIteratorTemplate<ArgIteratorBase>::GetNextOffset+0xda, calling clr!MetaSig::GetElemSize
000000f66cbff0d0 00007fffa314736d clr!MethodDescCallSite::CallTargetWorker+0xf8, calling clr!CallDescrWorkerWithHandler
000000f66cbff1d0 00007fffa322bf59 clr!ThreadNative::KickOffThread_Worker+0x109, calling clr!MethodDescCallSite::CallTargetWorker
000000f66cbff430 00007fffa3147ce5 clr!ManagedThreadBase_DispatchInner+0x39
000000f66cbff450 00007fffa8914146 mscoree!calloc_impl+0x72, calling ntdll!RtlAllocateHeap
000000f66cbff470 00007fffa3147c60 clr!ManagedThreadBase_DispatchMiddle+0x6c, calling clr!ManagedThreadBase_DispatchInner
000000f66cbff480 00007fffb813e5a1 ntdll!RtlpAllocateHeap+0xae1, calling ntdll!memset
000000f66cbff490 00007fffb53d7643 KERNELBASE!QuirkIsEnabled3+0x23, calling kernel32!QuirkIsEnabled3Worker
000000f66cbff4d0 00007fffa314a174 clr!Thread::SetLastThrownObject+0x35, calling clr!StressLog::LogOn
000000f66cbff540 00007fffa31473bd clr!FrameWithCookie<DebuggerU2MCatchHandlerFrame>::FrameWithCookie<DebuggerU2MCatchHandlerFrame>+0x26, calling clr!Frame::Push
000000f66cbff560 00007fffa31471cc clr!SystemDomain::GetAppDomainAtId+0x40, calling clr!AppDomain::CanThreadEnter
000000f66cbff570 00007fffa3147b9e clr!ManagedThreadBase_DispatchOuter+0x75, calling clr!ManagedThreadBase_DispatchMiddle
000000f66cbff590 00007fffa3146071 clr!ClrFlsIncrementValue+0x29
000000f66cbff600 00007fffa314a8c4 clr!ManagedThreadBase_DispatchInCorrectAD+0x15, calling clr!ManagedThreadBase_DispatchOuter
000000f66cbff630 00007fffa314a986 clr!Thread::DoADCallBack+0x278
000000f66cbff660 00007fffb766ed44 combase!ThreadFirstInitialize+0x78, calling combase!InitializeTlsApartmentAndEmptyContext
000000f66cbff670 00007fffb766fcc8 combase!RegisterThreadCleanupCallback+0x28, calling KERNELBASE!FlsSetValue
000000f66cbff6e0 00007fffb813af7e ntdll!RtlpAllocateHeapInternal+0xfe, calling ntdll!RtlpLowFragHeapAllocFromContext
000000f66cbff710 00007fffb766f44a combase!_CoInitializeEx+0x266, calling combase!__security_check_cookie
000000f66cbff720 00007fff9505618c *** ERROR: Symbol file could not be found.  Defaulted to export symbols for Microsoft.VisualStudio.Setup.Configuration.Native.dll - 
Microsoft_VisualStudio_Setup_Configuration_Native!GetSetupConfiguration+0x9f8c, calling Microsoft_VisualStudio_Setup_Configuration_Native!DllMain
000000f66cbff7f0 00007fffa314a8db clr!ManagedThreadBase_DispatchInner+0x2c37, calling clr!Thread::DoADCallBack
000000f66cbff820 00007fffa3146071 clr!ClrFlsIncrementValue+0x29
000000f66cbff830 00007fffa3147c60 clr!ManagedThreadBase_DispatchMiddle+0x6c, calling clr!ManagedThreadBase_DispatchInner
000000f66cbff840 00007fffa3678852 clr!DebuggerController::ControllerLockHolder::ControllerLockHolder+0x22, calling clr!CrstBase::CrstHolder::CrstHolder
000000f66cbff850 00007fffa31460a1 clr!CrstBase::Leave+0x65, calling clr!ClrFlsIncrementValue
000000f66cbff880 00007fffa37b46db clr!DebuggerController::EnableTraceCall+0x6f, calling clr!EEJitManager::CodeHeapIterator::~CodeHeapIterator
000000f66cbff900 00007fffa31473bd clr!FrameWithCookie<DebuggerU2MCatchHandlerFrame>::FrameWithCookie<DebuggerU2MCatchHandlerFrame>+0x26, calling clr!Frame::Push
000000f66cbff930 00007fffa3147b9e clr!ManagedThreadBase_DispatchOuter+0x75, calling clr!ManagedThreadBase_DispatchMiddle
000000f66cbff9c0 00007fffa3147d1f clr!ManagedThreadBase_FullTransitionWithAD+0x2f, calling clr!ManagedThreadBase_DispatchOuter
000000f66cbffa20 00007fffa322be3b clr!ThreadNative::KickOffThread+0xdb, calling clr!ManagedThreadBase_FullTransitionWithAD
000000f66cbffac0 00007fffa31469b9 clr!operator delete+0x29
000000f66cbffaf0 00007fffa330159f clr!Thread::intermediateThreadProc+0x86
000000f66cbffdf0 00007fffa330157f clr!Thread::intermediateThreadProc+0x66, calling clr!_chkstk
000000f66cbffe30 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66cbffe60 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
---------------------------------------------
Thread  48
Current frame: ntdll!NtWaitForMultipleObjects+0x14
Child-SP         RetAddr          Caller, Callee
000000f66ccfe0f0 00007fffb53bdcb0 KERNELBASE!WaitForMultipleObjectsEx+0xf0, calling ntdll!NtWaitForMultipleObjects
000000f66ccfe190 00007fffb53bddaf KERNELBASE!WaitForMultipleObjectsEx+0x1ef, calling ntdll!RtlActivateActivationContextUnsafeFast
000000f66ccfe1d0 00007fffa32ebc18 clr!MethodDesc::GetOrCreatePrecode+0x6c, calling clr!_security_check_cookie
000000f66ccfe210 00007fffa32ebc18 clr!MethodDesc::GetOrCreatePrecode+0x6c, calling clr!_security_check_cookie
000000f66ccfe240 00007fffa328ad5e clr!CExecutionEngine::ReleaseLock+0x6b, calling clr!ClrFlsIncrementValue
000000f66ccfe290 00007fffa314619f clr!ClrFlsGetValue+0x23
000000f66ccfe2b0 00007fffb76b7af5 combase!CObjectContext::QIHelper+0x105, calling ntdll!LdrpDispatchUserCallTarget
000000f66ccfe2c0 00007fffa3209e02 clr!IsDbgHelperSpecialThread+0xe, calling clr!ClrFlsGetValue
000000f66ccfe2e0 00007fffa314b38c clr!SafeQueryInterface+0xe8
000000f66ccfe2f0 00007fffa37ac127 clr!ThisIsHelperThreadWorker+0x23, calling clr!ThisIsTempHelperThread
000000f66ccfe320 00007fffa32b403c clr!SafeReleasePreemp+0x84
000000f66ccfe380 00007fffa32b57e5 clr!GetCurrentThreadTypeNT5+0xc5, calling clr!SafeReleasePreemp
000000f66ccfe3f0 00007fffa32e69ca clr!WaitForMultipleObjectsEx_SO_TOLERANT+0x62, calling kernel32!WaitForMultipleObjectsEx
000000f66ccfe420 00007fffa314ba42 clr!Thread::GetFinalApartment+0x1a, calling clr!Thread::GetApartment
000000f66ccfe450 00007fffa32e6864 clr!Thread::DoAppropriateWaitWorker+0x1e4, calling clr!WaitForMultipleObjectsEx_SO_TOLERANT
000000f66ccfe550 00007fffa32e665d clr!Thread::DoAppropriateWait+0x7d, calling clr!Thread::DoAppropriateWaitWorker
000000f66ccfe5c0 00007fffa3145e58 clr!HelperMethodFrame::Push+0x19, calling clr!GetThread
000000f66ccfe5d0 00007fffa370595f clr!WaitHandleNative::CorWaitMultipleNative+0x2af, calling clr!Thread::DoAppropriateWait
000000f66ccfe5f0 00007fffa3705820 clr!WaitHandleNative::CorWaitMultipleNative+0x170, calling clr!_chkstk
000000f66ccfe710 00007fffa32c283c clr!FC_GCPoll+0x2c, calling clr!GetThread
000000f66ccfe750 00007fff9efef67c (MethodDesc 00007fff9ebcb0e8 +0x9c System.Threading.WaitHandle.WaitAny(System.Threading.WaitHandle[], Int32, Boolean)), calling 00007fffa37056b0 (stub for System.Threading.WaitHandle.WaitMultiple(System.Threading.WaitHandle[], Int32, Boolean, Boolean))
000000f66ccfe7e8 00007fffa3705751 clr!WaitHandleNative::CorWaitMultipleNative+0xa1, calling clr!LazyMachStateCaptureState
000000f66ccfe850 00007fff9efef67c (MethodDesc 00007fff9ebcb0e8 +0x9c System.Threading.WaitHandle.WaitAny(System.Threading.WaitHandle[], Int32, Boolean)), calling 00007fffa37056b0 (stub for System.Threading.WaitHandle.WaitMultiple(System.Threading.WaitHandle[], Int32, Boolean, Boolean))
000000f66ccfe8b0 00007fff9dc2b2fb (MethodDesc 00007fff9da52300 +0x2eb System.Net.TimerThread.ThreadProc()), calling (MethodDesc 00007fff9ebcb0e8 +0 System.Threading.WaitHandle.WaitAny(System.Threading.WaitHandle[], Int32, Boolean))
000000f66ccfe960 00007fff9ef9ca72 (MethodDesc 00007fff9ebca660 +0x162 System.Threading.ExecutionContext.RunInternal(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean))
000000f66ccfea30 00007fff9ef9c904 (MethodDesc 00007fff9ebca650 +0x14 System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean)), calling (MethodDesc 00007fff9ebca660 +0 System.Threading.ExecutionContext.RunInternal(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean))
000000f66ccfea40 00007fffa3144675 clr!ThePreStub+0x55, calling clr!PreStubWorker
000000f66ccfea60 00007fff9ef9c8c2 (MethodDesc 00007fff9ebca640 +0x52 System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object)), calling (MethodDesc 00007fff9ebca650 +0 System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object, Boolean))
000000f66ccfeab0 00007fff9efd6472 (MethodDesc 00007fff9ebaa378 +0x52 System.Threading.ThreadHelper.ThreadStart()), calling (MethodDesc 00007fff9ebca640 +0 System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext, System.Threading.ContextCallback, System.Object))
000000f66ccfeaf0 00007fffa3146793 clr!CallDescrWorkerInternal+0x83
000000f66ccfeb30 00007fffa3146665 clr!CallDescrWorkerWithHandler+0x4e, calling clr!CallDescrWorkerInternal
000000f66ccfeb40 00007fffa31478a1 clr!ArgIteratorTemplate<ArgIteratorBase>::GetNextOffset+0x51, calling clr!ArgIteratorBase::IsVarArg
000000f66ccfeb50 00007fffa37ac390 clr!Debugger::TraceCall+0x80, calling clr!CLRException::HandlerState::CleanupTry
000000f66ccfeb70 00007fffa314736d clr!MethodDescCallSite::CallTargetWorker+0xf8, calling clr!CallDescrWorkerWithHandler
000000f66ccfec70 00007fffa322bf59 clr!ThreadNative::KickOffThread_Worker+0x109, calling clr!MethodDescCallSite::CallTargetWorker
000000f66ccfed40 00007fffb813cc38 ntdll!RtlpLowFragHeapAllocFromContext+0x378, calling ntdll!memset
000000f66ccfee70 00007fffb813af7e ntdll!RtlpAllocateHeapInternal+0xfe, calling ntdll!RtlpLowFragHeapAllocFromContext
000000f66ccfeed0 00007fffa3147ce5 clr!ManagedThreadBase_DispatchInner+0x39
000000f66ccfef10 00007fffa3147c60 clr!ManagedThreadBase_DispatchMiddle+0x6c, calling clr!ManagedThreadBase_DispatchInner
000000f66ccfef20 00007fffb813e5a1 ntdll!RtlpAllocateHeap+0xae1, calling ntdll!memset
000000f66ccfef70 00007fffa314a174 clr!Thread::SetLastThrownObject+0x35, calling clr!StressLog::LogOn
000000f66ccfefe0 00007fffa31473bd clr!FrameWithCookie<DebuggerU2MCatchHandlerFrame>::FrameWithCookie<DebuggerU2MCatchHandlerFrame>+0x26, calling clr!Frame::Push
000000f66ccff000 00007fffa31471cc clr!SystemDomain::GetAppDomainAtId+0x40, calling clr!AppDomain::CanThreadEnter
000000f66ccff010 00007fffa3147b9e clr!ManagedThreadBase_DispatchOuter+0x75, calling clr!ManagedThreadBase_DispatchMiddle
000000f66ccff030 00007fffa3146071 clr!ClrFlsIncrementValue+0x29
000000f66ccff0a0 00007fffa314a8c4 clr!ManagedThreadBase_DispatchInCorrectAD+0x15, calling clr!ManagedThreadBase_DispatchOuter
000000f66ccff0d0 00007fffa314a986 clr!Thread::DoADCallBack+0x278
000000f66ccff100 00007fffb766ed44 combase!ThreadFirstInitialize+0x78, calling combase!InitializeTlsApartmentAndEmptyContext
000000f66ccff110 00007fffb766fcc8 combase!RegisterThreadCleanupCallback+0x28, calling KERNELBASE!FlsSetValue
000000f66ccff180 00007fffb813af7e ntdll!RtlpAllocateHeapInternal+0xfe, calling ntdll!RtlpLowFragHeapAllocFromContext
000000f66ccff1b0 00007fffb766f44a combase!_CoInitializeEx+0x266, calling combase!__security_check_cookie
000000f66ccff290 00007fffa314a8db clr!ManagedThreadBase_DispatchInner+0x2c37, calling clr!Thread::DoADCallBack
000000f66ccff2c0 00007fffa3146071 clr!ClrFlsIncrementValue+0x29
000000f66ccff2d0 00007fffa3147c60 clr!ManagedThreadBase_DispatchMiddle+0x6c, calling clr!ManagedThreadBase_DispatchInner
000000f66ccff2e0 00007fffa3678852 clr!DebuggerController::ControllerLockHolder::ControllerLockHolder+0x22, calling clr!CrstBase::CrstHolder::CrstHolder
000000f66ccff2f0 00007fffa31460a1 clr!CrstBase::Leave+0x65, calling clr!ClrFlsIncrementValue
000000f66ccff320 00007fffa37b46db clr!DebuggerController::EnableTraceCall+0x6f, calling clr!EEJitManager::CodeHeapIterator::~CodeHeapIterator
000000f66ccff3a0 00007fffa31473bd clr!FrameWithCookie<DebuggerU2MCatchHandlerFrame>::FrameWithCookie<DebuggerU2MCatchHandlerFrame>+0x26, calling clr!Frame::Push
000000f66ccff3d0 00007fffa3147b9e clr!ManagedThreadBase_DispatchOuter+0x75, calling clr!ManagedThreadBase_DispatchMiddle
000000f66ccff460 00007fffa3147d1f clr!ManagedThreadBase_FullTransitionWithAD+0x2f, calling clr!ManagedThreadBase_DispatchOuter
000000f66ccff4c0 00007fffa322be3b clr!ThreadNative::KickOffThread+0xdb, calling clr!ManagedThreadBase_FullTransitionWithAD
000000f66ccff560 00007fffa31469b9 clr!operator delete+0x29
000000f66ccff590 00007fffa330159f clr!Thread::intermediateThreadProc+0x86
000000f66ccff910 00007fffa330157f clr!Thread::intermediateThreadProc+0x66, calling clr!_chkstk
000000f66ccff950 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66ccff980 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
---------------------------------------------
Thread  49
Current frame: ntdll!NtRemoveIoCompletion+0x14
Child-SP         RetAddr          Caller, Callee
000000f66ceff410 00007fffb53c9a8f KERNELBASE!GetQueuedCompletionStatus+0x3f, calling ntdll!NtRemoveIoCompletion
000000f66ceff470 00007fffa32a86dc clr!ThreadpoolMgr::CompletionPortThreadStart+0x210, calling kernel32!GetQueuedCompletionStatusStub
000000f66ceff510 00007fffa330159f clr!Thread::intermediateThreadProc+0x86
000000f66ceff520 00007fffb8189ad8 ntdll!LdrInitializeThunk+0x18, calling ntdll!NtContinue
000000f66ceff990 00007fffa330157f clr!Thread::intermediateThreadProc+0x66, calling clr!_chkstk
000000f66ceff9d0 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66ceffa00 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
---------------------------------------------
Thread  51
Current frame: ntdll!NtWaitForSingleObject+0x14
Child-SP         RetAddr          Caller, Callee
000000f66d03f190 00007fffb53a995f KERNELBASE!WaitForSingleObjectEx+0x9f, calling ntdll!NtWaitForSingleObject
000000f66d03f1a0 00007fffa32b403c clr!SafeReleasePreemp+0x84
000000f66d03f1b0 00007fffb816a670 ntdll!RtlSetLastWin32Error+0x40, calling ntdll!_security_check_cookie
000000f66d03f230 00007fffa314862e clr!CLRSemaphore::Wait+0x8a, calling kernel32!WaitForSingleObjectEx
000000f66d03f2a0 00007fffa33017e2 clr!Thread::SetApartment+0x1bd, calling clr!Thread::GetApartment
000000f66d03f2f0 00007fffa314892b clr!ThreadpoolMgr::UnfairSemaphore::Wait+0x115, calling clr!CLRSemaphore::Wait
000000f66d03f340 00007fffa314889a clr!ThreadpoolMgr::WorkerThreadStart+0x2bb, calling clr!ThreadpoolMgr::UnfairSemaphore::Wait
000000f66d03f3e0 00007fffa330159f clr!Thread::intermediateThreadProc+0x86
000000f66d03f440 00007fffb8189b1b ntdll!LdrpInitialize+0x3b, calling ntdll!_LdrpInitialize
000000f66d03f470 00007fffb8189ad8 ntdll!LdrInitializeThunk+0x18, calling ntdll!NtContinue
000000f66d03f8e0 00007fffa330157f clr!Thread::intermediateThreadProc+0x66, calling clr!_chkstk
000000f66d03f920 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66d03f950 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
---------------------------------------------
Thread  50
Current frame: ntdll!NtWaitForSingleObject+0x14
Child-SP         RetAddr          Caller, Callee
000000f66d0bf2b0 00007fffb53a995f KERNELBASE!WaitForSingleObjectEx+0x9f, calling ntdll!NtWaitForSingleObject
000000f66d0bf2c0 00007fffa32b403c clr!SafeReleasePreemp+0x84
000000f66d0bf2d0 00007fffb816a670 ntdll!RtlSetLastWin32Error+0x40, calling ntdll!_security_check_cookie
000000f66d0bf350 00007fffa314862e clr!CLRSemaphore::Wait+0x8a, calling kernel32!WaitForSingleObjectEx
000000f66d0bf3c0 00007fffa33017e2 clr!Thread::SetApartment+0x1bd, calling clr!Thread::GetApartment
000000f66d0bf410 00007fffa314892b clr!ThreadpoolMgr::UnfairSemaphore::Wait+0x115, calling clr!CLRSemaphore::Wait
000000f66d0bf460 00007fffa314889a clr!ThreadpoolMgr::WorkerThreadStart+0x2bb, calling clr!ThreadpoolMgr::UnfairSemaphore::Wait
000000f66d0bf500 00007fffa330159f clr!Thread::intermediateThreadProc+0x86
000000f66d0bf560 00007fffb8189bc6 ntdll!_LdrpInitialize+0x96, calling ntdll!NtTestAlert
000000f66d0bf5e0 00007fffb8189b1b ntdll!LdrpInitialize+0x3b, calling ntdll!_LdrpInitialize
000000f66d0bf610 00007fffb8189ad8 ntdll!LdrInitializeThunk+0x18, calling ntdll!NtContinue
000000f66d0bfa80 00007fffa330157f clr!Thread::intermediateThreadProc+0x66, calling clr!_chkstk
000000f66d0bfac0 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66d0bfaf0 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
---------------------------------------------
Thread  52
Current frame: ntdll!NtWaitForSingleObject+0x14
Child-SP         RetAddr          Caller, Callee
000000f66d13eed0 00007fffb53a995f KERNELBASE!WaitForSingleObjectEx+0x9f, calling ntdll!NtWaitForSingleObject
000000f66d13eee0 00007fffa32b403c clr!SafeReleasePreemp+0x84
000000f66d13eef0 00007fffb816a670 ntdll!RtlSetLastWin32Error+0x40, calling ntdll!_security_check_cookie
000000f66d13ef70 00007fffa314862e clr!CLRSemaphore::Wait+0x8a, calling kernel32!WaitForSingleObjectEx
000000f66d13efe0 00007fffa33017e2 clr!Thread::SetApartment+0x1bd, calling clr!Thread::GetApartment
000000f66d13f030 00007fffa314892b clr!ThreadpoolMgr::UnfairSemaphore::Wait+0x115, calling clr!CLRSemaphore::Wait
000000f66d13f080 00007fffa314889a clr!ThreadpoolMgr::WorkerThreadStart+0x2bb, calling clr!ThreadpoolMgr::UnfairSemaphore::Wait
000000f66d13f0e0 00007fffb81583ff ntdll!LdrpReleaseLoaderLock+0x33, calling ntdll!RtlGetCurrentServiceSessionId
000000f66d13f120 00007fffa330159f clr!Thread::intermediateThreadProc+0x86
000000f66d13f190 00007fffb8132175 ntdll!LdrpInitializeThread+0x135, calling ntdll!RtlActivateActivationContextUnsafeFast
000000f66d13f198 00007fffb81321a8 ntdll!LdrpInitializeThread+0x168, calling ntdll!RtlDeactivateActivationContextUnsafeFast
000000f66d13f200 00007fffb8189bc6 ntdll!_LdrpInitialize+0x96, calling ntdll!NtTestAlert
000000f66d13f280 00007fffb8189b1b ntdll!LdrpInitialize+0x3b, calling ntdll!_LdrpInitialize
000000f66d13f2b0 00007fffb8189ad8 ntdll!LdrInitializeThunk+0x18, calling ntdll!NtContinue
000000f66d13f720 00007fffa330157f clr!Thread::intermediateThreadProc+0x66, calling clr!_chkstk
000000f66d13f760 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66d13f790 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
---------------------------------------------
Thread  53
Current frame: ntdll!NtWaitForSingleObject+0x14
Child-SP         RetAddr          Caller, Callee
000000f66d1bf2e0 00007fffb53a995f KERNELBASE!WaitForSingleObjectEx+0x9f, calling ntdll!NtWaitForSingleObject
000000f66d1bf2f0 00007fffa32b403c clr!SafeReleasePreemp+0x84
000000f66d1bf300 00007fffb816a670 ntdll!RtlSetLastWin32Error+0x40, calling ntdll!_security_check_cookie
000000f66d1bf380 00007fffa314862e clr!CLRSemaphore::Wait+0x8a, calling kernel32!WaitForSingleObjectEx
000000f66d1bf3f0 00007fffa33017e2 clr!Thread::SetApartment+0x1bd, calling clr!Thread::GetApartment
000000f66d1bf440 00007fffa314892b clr!ThreadpoolMgr::UnfairSemaphore::Wait+0x115, calling clr!CLRSemaphore::Wait
000000f66d1bf490 00007fffa314889a clr!ThreadpoolMgr::WorkerThreadStart+0x2bb, calling clr!ThreadpoolMgr::UnfairSemaphore::Wait
000000f66d1bf4e0 00007fffa79390df dbghelp!dllmain_dispatch+0x8f, calling dbghelp!DllMain
000000f66d1bf530 00007fffa330159f clr!Thread::intermediateThreadProc+0x86
000000f66d1bf570 00007fffb81583ff ntdll!LdrpReleaseLoaderLock+0x33, calling ntdll!RtlGetCurrentServiceSessionId
000000f66d1bf580 00007fffb815cd4c ntdll!LdrpDropLastInProgressCount+0x38, calling ntdll!RtlLeaveCriticalSection
000000f66d1bf5b0 00007fffb813223d ntdll!LdrpInitializeThread+0x1fd, calling ntdll!LdrpDropLastInProgressCount
000000f66d1bf620 00007fffb8132175 ntdll!LdrpInitializeThread+0x135, calling ntdll!RtlActivateActivationContextUnsafeFast
000000f66d1bf628 00007fffb81321a8 ntdll!LdrpInitializeThread+0x168, calling ntdll!RtlDeactivateActivationContextUnsafeFast
000000f66d1bf690 00007fffb8189bc6 ntdll!_LdrpInitialize+0x96, calling ntdll!NtTestAlert
000000f66d1bf710 00007fffb8189b1b ntdll!LdrpInitialize+0x3b, calling ntdll!_LdrpInitialize
000000f66d1bf740 00007fffb8189ad8 ntdll!LdrInitializeThunk+0x18, calling ntdll!NtContinue
000000f66d1bfbb0 00007fffa330157f clr!Thread::intermediateThreadProc+0x66, calling clr!_chkstk
000000f66d1bfbf0 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66d1bfc20 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
---------------------------------------------
Thread  54
Current frame: ntdll!NtWaitForSingleObject+0x14
Child-SP         RetAddr          Caller, Callee
000000f66d23f310 00007fffb53a995f KERNELBASE!WaitForSingleObjectEx+0x9f, calling ntdll!NtWaitForSingleObject
000000f66d23f320 00007fffa32b403c clr!SafeReleasePreemp+0x84
000000f66d23f330 00007fffb816a670 ntdll!RtlSetLastWin32Error+0x40, calling ntdll!_security_check_cookie
000000f66d23f3b0 00007fffa314862e clr!CLRSemaphore::Wait+0x8a, calling kernel32!WaitForSingleObjectEx
000000f66d23f420 00007fffa33017e2 clr!Thread::SetApartment+0x1bd, calling clr!Thread::GetApartment
000000f66d23f470 00007fffa314892b clr!ThreadpoolMgr::UnfairSemaphore::Wait+0x115, calling clr!CLRSemaphore::Wait
000000f66d23f4c0 00007fffa314889a clr!ThreadpoolMgr::WorkerThreadStart+0x2bb, calling clr!ThreadpoolMgr::UnfairSemaphore::Wait
000000f66d23f560 00007fffa330159f clr!Thread::intermediateThreadProc+0x86
000000f66d23f590 00007fffa79390df dbghelp!dllmain_dispatch+0x8f, calling dbghelp!DllMain
000000f66d23f5b0 00007fff9e701163 clrjit!__DllMainCRTStartup+0xa3, calling clrjit!DllMain
000000f66d23f5c0 00007fffa88e1af7 mscoree!CorDllMain_Exported+0x37, calling mscoree!ShellShim__CorDllMain
000000f66d23f5f0 00007fffb813485f ntdll!LdrpCallInitRoutine+0x6b
000000f66d23f620 00007fffb81583ff ntdll!LdrpReleaseLoaderLock+0x33, calling ntdll!RtlGetCurrentServiceSessionId
000000f66d23f630 00007fffb815cd4c ntdll!LdrpDropLastInProgressCount+0x38, calling ntdll!RtlLeaveCriticalSection
000000f66d23f660 00007fffb813223d ntdll!LdrpInitializeThread+0x1fd, calling ntdll!LdrpDropLastInProgressCount
000000f66d23f6d0 00007fffb8132175 ntdll!LdrpInitializeThread+0x135, calling ntdll!RtlActivateActivationContextUnsafeFast
000000f66d23f6d8 00007fffb81321a8 ntdll!LdrpInitializeThread+0x168, calling ntdll!RtlDeactivateActivationContextUnsafeFast
000000f66d23f740 00007fffb8189bc6 ntdll!_LdrpInitialize+0x96, calling ntdll!NtTestAlert
000000f66d23f7c0 00007fffb8189b1b ntdll!LdrpInitialize+0x3b, calling ntdll!_LdrpInitialize
000000f66d23f7f0 00007fffb8189ad8 ntdll!LdrInitializeThunk+0x18, calling ntdll!NtContinue
000000f66d23fc60 00007fffa330157f clr!Thread::intermediateThreadProc+0x66, calling clr!_chkstk
000000f66d23fca0 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66d23fcd0 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
---------------------------------------------
Thread  55
Current frame: ntdll!NtWaitForSingleObject+0x14
Child-SP         RetAddr          Caller, Callee
000000f66d2bee10 00007fffb53a995f KERNELBASE!WaitForSingleObjectEx+0x9f, calling ntdll!NtWaitForSingleObject
000000f66d2bee20 00007fffa32b403c clr!SafeReleasePreemp+0x84
000000f66d2bee30 00007fffb816a670 ntdll!RtlSetLastWin32Error+0x40, calling ntdll!_security_check_cookie
000000f66d2beeb0 00007fffa314862e clr!CLRSemaphore::Wait+0x8a, calling kernel32!WaitForSingleObjectEx
000000f66d2bef20 00007fffa33017e2 clr!Thread::SetApartment+0x1bd, calling clr!Thread::GetApartment
000000f66d2bef70 00007fffa314892b clr!ThreadpoolMgr::UnfairSemaphore::Wait+0x115, calling clr!CLRSemaphore::Wait
000000f66d2befc0 00007fffa314889a clr!ThreadpoolMgr::WorkerThreadStart+0x2bb, calling clr!ThreadpoolMgr::UnfairSemaphore::Wait
000000f66d2bf060 00007fffa330159f clr!Thread::intermediateThreadProc+0x86
000000f66d2bf0b0 00007fffa79394ba dbghelp!_scrt_dllmain_crt_thread_attach+0x16, calling dbghelp!TMEQTS::IsTMEQTS
000000f66d2bf0d0 00007fffb8134777 ntdll!RtlDeactivateActivationContextUnsafeFast+0xc7, calling ntdll!_security_check_cookie
000000f66d2bf0e0 00007fffa7938e8d dbghelp!dllmain_crt_dispatch+0x2d, calling dbghelp!_scrt_dllmain_crt_thread_attach
000000f66d2bf100 00007fffb8132313 ntdll!RtlActivateActivationContextUnsafeFast+0x93, calling ntdll!_security_check_cookie
000000f66d2bf110 00007fffa79390df dbghelp!dllmain_dispatch+0x8f, calling dbghelp!DllMain
000000f66d2bf130 00007fff9e701163 clrjit!__DllMainCRTStartup+0xa3, calling clrjit!DllMain
000000f66d2bf140 00007fffa88e1af7 mscoree!CorDllMain_Exported+0x37, calling mscoree!ShellShim__CorDllMain
000000f66d2bf170 00007fffb813485f ntdll!LdrpCallInitRoutine+0x6b
000000f66d2bf1a0 00007fffb81583ff ntdll!LdrpReleaseLoaderLock+0x33, calling ntdll!RtlGetCurrentServiceSessionId
000000f66d2bf1b0 00007fffb815cd4c ntdll!LdrpDropLastInProgressCount+0x38, calling ntdll!RtlLeaveCriticalSection
000000f66d2bf1e0 00007fffb813223d ntdll!LdrpInitializeThread+0x1fd, calling ntdll!LdrpDropLastInProgressCount
000000f66d2bf250 00007fffb8132175 ntdll!LdrpInitializeThread+0x135, calling ntdll!RtlActivateActivationContextUnsafeFast
000000f66d2bf258 00007fffb81321a8 ntdll!LdrpInitializeThread+0x168, calling ntdll!RtlDeactivateActivationContextUnsafeFast
000000f66d2bf2c0 00007fffb8189bc6 ntdll!_LdrpInitialize+0x96, calling ntdll!NtTestAlert
000000f66d2bf340 00007fffb8189b1b ntdll!LdrpInitialize+0x3b, calling ntdll!_LdrpInitialize
000000f66d2bf370 00007fffb8189ad8 ntdll!LdrInitializeThunk+0x18, calling ntdll!NtContinue
000000f66d2bf7e0 00007fffa330157f clr!Thread::intermediateThreadProc+0x66, calling clr!_chkstk
000000f66d2bf820 00007fffb7d92774 kernel32!BaseThreadInitThunk+0x14, calling ntdll!LdrpDispatchUserCallTarget
000000f66d2bf850 00007fffb8180d51 ntdll!RtlUserThreadStart+0x21, calling ntdll!LdrpDispatchUserCallTarget
";

        #endregion
    }
}