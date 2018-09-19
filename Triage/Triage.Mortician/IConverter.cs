using Microsoft.Diagnostics.Runtime.Utilities;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrMd = Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    internal interface IConverter
    {
        Architecture Convert(ClrMd.Architecture architecture);
        IBlockingObject Convert(ClrMd.BlockingObject blockingObject);
        BlockingReason Convert(ClrMd.BlockingReason blockingReason);
        ICcwData Convert(ClrMd.CcwData data);
        IClrAppDomain Convert(ClrMd.ClrAppDomain appDomain);
        ClrElementType Convert(ClrMd.ClrElementType clrElementType);
        IClrException Convert(ClrMd.ClrException exception);
        IClrStaticField Convert(ClrMd.ClrStaticField field);
        ClrFlavor Convert(ClrMd.ClrFlavor flavor);
        IClrHandle Convert(ClrMd.ClrHandle handle);
        IClrHeap Convert(ClrMd.ClrHeap heap);
        IClrInfo Convert(ClrMd.ClrInfo info);
        IClrInstanceField Convert(ClrMd.ClrInstanceField instanceField);
        IClrInterface Convert(ClrMd.ClrInterface iface);
        IClrMemoryRegion Convert(ClrMd.ClrMemoryRegion memoryRegion);
        ClrMemoryRegionType Convert(ClrMd.ClrMemoryRegionType memoryRegionType);
        IClrMethod Convert(ClrMd.ClrMethod method);
        IClrModule Convert(ClrMd.ClrModule module);
        IClrObject Convert(ClrMd.ClrObject o);
        IClrRoot Convert(ClrMd.ClrRoot root);
        ClrRootStackwalkPolicy Convert(ClrMd.ClrRootStackwalkPolicy policy);
        IClrRuntime Convert(ClrMd.ClrRuntime runtime);
        IClrSegment Convert(ClrMd.ClrSegment segment);
        IClrStackFrame Convert(ClrMd.ClrStackFrame frame);
        ClrStackFrameType Convert(ClrMd.ClrStackFrameType type);
        IClrThread Convert(ClrMd.ClrThread thread);
        IClrThreadStaticField Convert(ClrMd.ClrThreadStaticField field);
        IClrThreadPool Convert(ClrMd.ClrThreadPool pool);
        IClrType Convert(ClrMd.ClrType type);
        IClrValueClass Convert(ClrMd.ClrValueClass valueClass);
        IComInterfaceData Convert(ClrMd.ComInterfaceData interfaceData);
        IDacInfo Convert(ClrMd.DacInfo dacInfo);
        IDataTarget Convert(ClrMd.DataTarget dataTarget);
        IFileVersionInfo Convert(FileVersionInfo fileVersionInfo);
        GcMode Convert(ClrMd.GcMode mode);
        IGcRoot Convert(ClrMd.GCRoot gcRoot);
        GcRootKind Convert(ClrMd.GCRootKind kind);
        GcRootProgressEvent Convert(ClrMd.GCRootProgressEvent rootProgressEvent);
        GcSegmentType Convert(ClrMd.GCSegmentType segmentType);
        HandleType Convert(ClrMd.HandleType handleType);
        IHotColdRegions Convert(ClrMd.HotColdRegions hotColdRegions);
        IDataReader Convert(ClrMd.IDataReader dataReader);
        IILInfo Convert(ClrMd.ILInfo info);
        ILToNativeMap Convert(ClrMd.ILToNativeMap map);
        ISymbolProvider Convert(ClrMd.ISymbolProvider provider);
        ISymbolResolver Convert(ClrMd.ISymbolResolver resolver);
        IManagedWorkItem Convert(ClrMd.ManagedWorkItem workItem);
        MethodCompilationType Convert(ClrMd.MethodCompilationType compilationType);
        IModuleInfo Convert(ClrMd.ModuleInfo moduleInfo);
        INativeWorkItem Convert(ClrMd.NativeWorkItem nativeWorkItem);
        IObjectSet Convert(ClrMd.ObjectSet objectSet);
        IPdbInfo Convert(ClrMd.PdbInfo info);
        IPeFile Convert(PEFile peFile);
        IRcwData Convert(ClrMd.RcwData data);
        IRootPath Convert(ClrMd.RootPath path);
        ISymbolLocator Convert(SymbolLocator locator);
        VersionInfo Convert(ClrMd.VersionInfo versionInfo);
        VirtualQueryData Convert(ClrMd.VirtualQueryData data);
        WorkItemKind Convert(ClrMd.WorkItemKind kind);
    }
}