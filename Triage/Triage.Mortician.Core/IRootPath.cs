namespace Microsoft.Diagnostics.Runtime
{
    public interface IRootPath
    {
        /// <summary>
        /// The location that roots the object.
        /// </summary>
        ClrRoot Root { get; set; }

        /// <summary>
        /// The path from Root to a given target object.
        /// </summary>
        ClrObject[] Path { get; set; }
    }
}