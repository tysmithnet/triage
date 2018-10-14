// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-20-2018
// ***********************************************************************
// <copyright file="IConverter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.Diagnostics.Runtime.Utilities;
using Mortician.Core.ClrMdAbstractions;
using ClrMd = Microsoft.Diagnostics.Runtime;

namespace Mortician
{
    /// <summary>
    ///     Interface IConverter
    /// </summary>
    internal interface IConverter
    {
        /// <summary>
        ///     Converts the specified architecture.
        /// </summary>
        /// <param name="architecture">The architecture.</param>
        /// <returns>Architecture.</returns>
        Architecture Convert(ClrMd.Architecture architecture);

        /// <summary>
        ///     Converts the specified blocking object.
        /// </summary>
        /// <param name="blockingObject">The blocking object.</param>
        /// <returns>IBlockingObject.</returns>
        IBlockingObject Convert(ClrMd.BlockingObject blockingObject);

        /// <summary>
        ///     Converts the specified blocking reason.
        /// </summary>
        /// <param name="blockingReason">The blocking reason.</param>
        /// <returns>BlockingReason.</returns>
        BlockingReason Convert(ClrMd.BlockingReason blockingReason);

        /// <summary>
        ///     Converts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>ICcwData.</returns>
        ICcwData Convert(ClrMd.CcwData data);

        /// <summary>
        ///     Converts the specified application domain.
        /// </summary>
        /// <param name="appDomain">The application domain.</param>
        /// <returns>IClrAppDomain.</returns>
        IClrAppDomain Convert(ClrMd.ClrAppDomain appDomain);

        /// <summary>
        ///     Converts the specified color element type.
        /// </summary>
        /// <param name="clrElementType">Type of the color element.</param>
        /// <returns>ClrElementType.</returns>
        ClrElementType Convert(ClrMd.ClrElementType clrElementType);

        /// <summary>
        ///     Converts the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>IClrException.</returns>
        IClrException Convert(ClrMd.ClrException exception);

        /// <summary>
        ///     Converts the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>IClrStaticField.</returns>
        IClrStaticField Convert(ClrMd.ClrStaticField field);

        /// <summary>
        ///     Converts the specified flavor.
        /// </summary>
        /// <param name="flavor">The flavor.</param>
        /// <returns>ClrFlavor.</returns>
        ClrFlavor Convert(ClrMd.ClrFlavor flavor);

        /// <summary>
        ///     Converts the specified handle.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <returns>IClrHandle.</returns>
        IClrHandle Convert(ClrMd.ClrHandle handle);

        /// <summary>
        ///     Converts the specified heap.
        /// </summary>
        /// <param name="heap">The heap.</param>
        /// <returns>IClrHeap.</returns>
        IClrHeap Convert(ClrMd.ClrHeap heap);

        /// <summary>
        ///     Converts the specified information.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns>IClrInfo.</returns>
        IClrInfo Convert(ClrMd.ClrInfo info);

        /// <summary>
        ///     Converts the specified instance field.
        /// </summary>
        /// <param name="instanceField">The instance field.</param>
        /// <returns>IClrInstanceField.</returns>
        IClrInstanceField Convert(ClrMd.ClrInstanceField instanceField);

        /// <summary>
        ///     Converts the specified iface.
        /// </summary>
        /// <param name="iface">The iface.</param>
        /// <returns>IClrInterface.</returns>
        IClrInterface Convert(ClrMd.ClrInterface iface);

        /// <summary>
        ///     Converts the specified memory region.
        /// </summary>
        /// <param name="memoryRegion">The memory region.</param>
        /// <returns>IClrMemoryRegion.</returns>
        IClrMemoryRegion Convert(ClrMd.ClrMemoryRegion memoryRegion);

        /// <summary>
        ///     Converts the specified memory region type.
        /// </summary>
        /// <param name="memoryRegionType">Type of the memory region.</param>
        /// <returns>ClrMemoryRegionType.</returns>
        ClrMemoryRegionType Convert(ClrMd.ClrMemoryRegionType memoryRegionType);

        /// <summary>
        ///     Converts the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>IClrMethod.</returns>
        IClrMethod Convert(ClrMd.ClrMethod method);

        /// <summary>
        ///     Converts the specified module.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <returns>IClrModule.</returns>
        IClrModule Convert(ClrMd.ClrModule module);

        /// <summary>
        ///     Converts the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns>IClrObject.</returns>
        IClrObject Convert(ClrMd.ClrObject o);

        /// <summary>
        ///     Converts the specified root.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns>IClrRoot.</returns>
        IClrRoot Convert(ClrMd.ClrRoot root);

        /// <summary>
        ///     Converts the specified policy.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns>ClrRootStackwalkPolicy.</returns>
        ClrRootStackwalkPolicy Convert(ClrMd.ClrRootStackwalkPolicy policy);

        /// <summary>
        ///     Converts the specified runtime.
        /// </summary>
        /// <param name="runtime">The runtime.</param>
        /// <returns>IClrRuntime.</returns>
        IClrRuntime Convert(ClrMd.ClrRuntime runtime);

        /// <summary>
        ///     Converts the specified segment.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns>IClrSegment.</returns>
        IClrSegment Convert(ClrMd.ClrSegment segment);

        /// <summary>
        ///     Converts the specified frame.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <returns>IClrStackFrame.</returns>
        IClrStackFrame Convert(ClrMd.ClrStackFrame frame);

        /// <summary>
        ///     Converts the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>ClrStackFrameType.</returns>
        ClrStackFrameType Convert(ClrMd.ClrStackFrameType type);

        /// <summary>
        ///     Converts the specified thread.
        /// </summary>
        /// <param name="thread">The thread.</param>
        /// <returns>IClrThread.</returns>
        IClrThread Convert(ClrMd.ClrThread thread);

        /// <summary>
        ///     Converts the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>IClrThreadStaticField.</returns>
        IClrThreadStaticField Convert(ClrMd.ClrThreadStaticField field);

        /// <summary>
        ///     Converts the specified pool.
        /// </summary>
        /// <param name="pool">The pool.</param>
        /// <returns>IClrThreadPool.</returns>
        IClrThreadPool Convert(ClrMd.ClrThreadPool pool);

        /// <summary>
        ///     Converts the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>IClrType.</returns>
        IClrType Convert(ClrMd.ClrType type);

        /// <summary>
        ///     Converts the specified value class.
        /// </summary>
        /// <param name="valueClass">The value class.</param>
        /// <returns>IClrValueClass.</returns>
        IClrValueClass Convert(ClrMd.ClrValueClass valueClass);

        /// <summary>
        ///     Converts the specified interface data.
        /// </summary>
        /// <param name="interfaceData">The interface data.</param>
        /// <returns>IComInterfaceData.</returns>
        IComInterfaceData Convert(ClrMd.ComInterfaceData interfaceData);

        /// <summary>
        ///     Converts the specified dac information.
        /// </summary>
        /// <param name="dacInfo">The dac information.</param>
        /// <returns>IDacInfo.</returns>
        IDacInfo Convert(ClrMd.DacInfo dacInfo);

        /// <summary>
        ///     Converts the specified data target.
        /// </summary>
        /// <param name="dataTarget">The data target.</param>
        /// <returns>IDataTarget.</returns>
        IDataTarget Convert(ClrMd.DataTarget dataTarget);

        /// <summary>
        ///     Converts the specified file version information.
        /// </summary>
        /// <param name="fileVersionInfo">The file version information.</param>
        /// <returns>IFileVersionInfo.</returns>
        IFileVersionInfo Convert(FileVersionInfo fileVersionInfo);

        /// <summary>
        ///     Converts the specified mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>GcMode.</returns>
        GcMode Convert(ClrMd.GcMode mode);

        /// <summary>
        ///     Converts the specified gc root.
        /// </summary>
        /// <param name="gcRoot">The gc root.</param>
        /// <returns>IGcRoot.</returns>
        IGcRoot Convert(ClrMd.GCRoot gcRoot);

        /// <summary>
        ///     Converts the specified kind.
        /// </summary>
        /// <param name="kind">The kind.</param>
        /// <returns>GcRootKind.</returns>
        GcRootKind Convert(ClrMd.GCRootKind kind);

        /// <summary>
        ///     Converts the specified root progress event.
        /// </summary>
        /// <param name="rootProgressEvent">The root progress event.</param>
        /// <returns>GcRootProgressEvent.</returns>
        GcRootProgressEvent Convert(ClrMd.GCRootProgressEvent rootProgressEvent);

        /// <summary>
        ///     Converts the specified segment type.
        /// </summary>
        /// <param name="segmentType">Type of the segment.</param>
        /// <returns>GcSegmentType.</returns>
        GcSegmentType Convert(ClrMd.GCSegmentType segmentType);

        /// <summary>
        ///     Converts the specified handle type.
        /// </summary>
        /// <param name="handleType">Type of the handle.</param>
        /// <returns>HandleType.</returns>
        HandleType Convert(ClrMd.HandleType handleType);

        /// <summary>
        ///     Converts the specified hot cold regions.
        /// </summary>
        /// <param name="hotColdRegions">The hot cold regions.</param>
        /// <returns>IHotColdRegions.</returns>
        IHotColdRegions Convert(ClrMd.HotColdRegions hotColdRegions);

        /// <summary>
        ///     Converts the specified data reader.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>IDataReader.</returns>
        IDataReader Convert(ClrMd.IDataReader dataReader);

        /// <summary>
        ///     Converts the specified information.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns>IILInfo.</returns>
        IILInfo Convert(ClrMd.ILInfo info);

        /// <summary>
        ///     Converts the specified map.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <returns>ILToNativeMap.</returns>
        ILToNativeMap Convert(ClrMd.ILToNativeMap map);

        /// <summary>
        ///     Converts the specified provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>ISymbolProvider.</returns>
        ISymbolProvider Convert(ClrMd.ISymbolProvider provider);

        /// <summary>
        ///     Converts the specified resolver.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        /// <returns>ISymbolResolver.</returns>
        ISymbolResolver Convert(ClrMd.ISymbolResolver resolver);

        /// <summary>
        ///     Converts the specified work item.
        /// </summary>
        /// <param name="workItem">The work item.</param>
        /// <returns>IManagedWorkItem.</returns>
        IManagedWorkItem Convert(ClrMd.ManagedWorkItem workItem);

        /// <summary>
        ///     Converts the specified compilation type.
        /// </summary>
        /// <param name="compilationType">Type of the compilation.</param>
        /// <returns>MethodCompilationType.</returns>
        MethodCompilationType Convert(ClrMd.MethodCompilationType compilationType);

        /// <summary>
        ///     Converts the specified module information.
        /// </summary>
        /// <param name="moduleInfo">The module information.</param>
        /// <returns>IModuleInfo.</returns>
        IModuleInfo Convert(ClrMd.ModuleInfo moduleInfo);

        /// <summary>
        ///     Converts the specified native work item.
        /// </summary>
        /// <param name="nativeWorkItem">The native work item.</param>
        /// <returns>INativeWorkItem.</returns>
        INativeWorkItem Convert(ClrMd.NativeWorkItem nativeWorkItem);

        /// <summary>
        ///     Converts the specified object set.
        /// </summary>
        /// <param name="objectSet">The object set.</param>
        /// <returns>IObjectSet.</returns>
        IObjectSet Convert(ClrMd.ObjectSet objectSet);

        /// <summary>
        ///     Converts the specified information.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns>IPdbInfo.</returns>
        IPdbInfo Convert(ClrMd.PdbInfo info);

        /// <summary>
        ///     Converts the specified pe file.
        /// </summary>
        /// <param name="peFile">The pe file.</param>
        /// <returns>IPeFile.</returns>
        IPeFile Convert(PEFile peFile);

        /// <summary>
        ///     Converts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>IRcwData.</returns>
        IRcwData Convert(ClrMd.RcwData data);

        /// <summary>
        ///     Converts the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>IRootPath.</returns>
        IRootPath Convert(ClrMd.RootPath path);

        /// <summary>
        ///     Converts the specified locator.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <returns>ISymbolLocator.</returns>
        ISymbolLocator Convert(SymbolLocator locator);

        /// <summary>
        ///     Converts the specified version information.
        /// </summary>
        /// <param name="versionInfo">The version information.</param>
        /// <returns>VersionInfo.</returns>
        VersionInfo Convert(ClrMd.VersionInfo versionInfo);

        /// <summary>
        ///     Converts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>VirtualQueryData.</returns>
        VirtualQueryData Convert(ClrMd.VirtualQueryData data);

        /// <summary>
        ///     Converts the specified kind.
        /// </summary>
        /// <param name="kind">The kind.</param>
        /// <returns>WorkItemKind.</returns>
        WorkItemKind Convert(ClrMd.WorkItemKind kind);
    }
}