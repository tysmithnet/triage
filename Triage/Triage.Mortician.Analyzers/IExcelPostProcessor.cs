using System.IO;

namespace Triage.Mortician.Analyzers
{
    /// <summary>
    ///     Represents an object that can do post processing on
    /// </summary>
    public interface IExcelPostProcessor
    {
        void PostProcess(FileInfo reportFile);
    }
}