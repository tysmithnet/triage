using System.Linq;
using FluentAssertions;
using Triage.Mortician.Reports;
using Xunit;

namespace Triage.Mortician.Test
{
    public class EeStackOutputProcessor_Should
    {
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

        [Fact]
        public void Account_For_Frames_With_No_Callee()
        {
            // arrange
            var processor = new EeStackOutputProcessor();

            // act
            var report = processor.ProcessOutput(HELLO_WORLD);

            // assert
            report.ThreadsInternal.Count.Should().Be(2);
            report.ThreadsInternal[0].StackFramesInternal.First(x => x.Caller == "clr!CallDescrWorkerInternal+0x83")
                .Callee.Should().BeNull();
        }
    }
}