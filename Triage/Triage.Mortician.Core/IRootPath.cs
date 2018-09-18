namespace Triage.Mortician.Core
{
    public interface IRootPath
    {
        /// <summary>
        /// The location that roots the object.
        /// </summary>
        IClrRoot Root { get; set; }

        /// <summary>
        /// The path from Root to a given target object.
        /// </summary>
        IClrObject[] Path { get; set; }
    }
}