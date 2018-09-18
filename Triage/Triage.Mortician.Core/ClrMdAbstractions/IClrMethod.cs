using System.Collections.Generic;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    public enum MethodCompilationType
    {
        /// <summary>
        /// Method is not yet JITed and no NGEN image exists.
        /// </summary>
        None,

        /// <summary>
        /// Method was JITed.
        /// </summary>
        Jit,

        /// <summary>
        /// Method was NGEN'ed (pre-JITed).
        /// </summary>
        Ngen
    }

    public interface IClrMethod
    {
        /// <summary>
        /// Retrieves the first MethodDesc in EnumerateMethodDescs().  For single
        /// AppDomain programs this is the only MethodDesc.  MethodDescs
        /// are unique to an Method/AppDomain pair, so when there are multiple domains
        /// there will be multiple MethodDescs for a method.
        /// </summary>
        ulong MethodDesc { get; }

        /// <summary>
        /// The name of the method.  For example, "void System.Foo.Bar(object o, int i)" would return "Bar".
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns the instruction pointer in the target process for the start of the method's assembly.
        /// </summary>
        ulong NativeCode { get; }

        /// <summary>
        /// Returns the location in memory of the IL for this method.
        /// </summary>
        IILInfo IL { get; }

        /// <summary>
        /// Returns the regions of memory that 
        /// </summary>
        IHotColdRegions HotColdInfo { get; }

        /// <summary>
        /// Returns the way this method was compiled.
        /// </summary>
        MethodCompilationType CompilationType { get; }

        /// <summary>
        /// Returns the IL to native offset mapping.
        /// </summary>
        ILToNativeMap[] ILOffsetMap { get; }

        /// <summary>
        /// Returns the metadata token of the current method.
        /// </summary>
        uint MetadataToken { get; }

        /// <summary>
        /// Returns the enclosing type of this method.
        /// </summary>
        IClrType Type { get; }

        /// <summary>
        /// Returns if this method is public.
        /// </summary>
        bool IsPublic { get; }

        /// <summary>
        /// Returns if this method is private.
        /// </summary>
        bool IsPrivate { get; }

        /// <summary>
        /// Returns if this method is internal.
        /// </summary>
        bool IsInternal { get; }

        /// <summary>
        /// Returns if this method is protected.
        /// </summary>
        bool IsProtected { get; }

        /// <summary>
        /// Returns if this method is static.
        /// </summary>
        bool IsStatic { get; }

        /// <summary>
        /// Returns if this method is final.
        /// </summary>
        bool IsFinal { get; }

        /// <summary>
        /// Returns if this method is a PInvoke.
        /// </summary>
        bool IsPInvoke { get; }

        /// <summary>
        /// Returns if this method is a special method.
        /// </summary>
        bool IsSpecialName { get; }

        /// <summary>
        /// Returns if this method is runtime special method.
        /// </summary>
        bool IsRTSpecialName { get; }

        /// <summary>
        /// Returns if this method is virtual.
        /// </summary>
        bool IsVirtual { get; }

        /// <summary>
        /// Returns if this method is abstract.
        /// </summary>
        bool IsAbstract { get; }

        /// <summary>
        /// Returns the location of the GCInfo for this method.
        /// </summary>
        ulong GCInfo { get; }

        /// <summary>
        /// Returns whether this method is an instance constructor.
        /// </summary>
        bool IsConstructor { get; }

        /// <summary>
        /// Returns whether this method is a static constructor.
        /// </summary>
        bool IsClassConstructor { get; }

        /// <summary>
        /// Enumerates all method descs for this method in the process.  MethodDescs
        /// are unique to an Method/AppDomain pair, so when there are multiple domains
        /// there will be multiple MethodDescs for a method.
        /// </summary>
        /// <returns>An enumeration of method handles in the process for this given
        /// method.</returns>
        IEnumerable<ulong> EnumerateMethodDescs();

        /// <summary>
        /// Returns the full signature of the function.  For example, "void System.Foo.Bar(object o, int i)"
        /// would return "System.Foo.Bar(System.Object, System.Int32)"
        /// </summary>
        string GetFullSignature();

        /// <summary>
        /// Gets the ILOffset of the given address within this method.
        /// </summary>
        /// <param name="addr">The absolute address of the code (not a relative offset).</param>
        /// <returns>The IL offset of the given address.</returns>
        int GetILOffset(ulong addr);
    }
}