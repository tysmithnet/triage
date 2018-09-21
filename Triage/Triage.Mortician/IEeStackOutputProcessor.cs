namespace Triage.Mortician
{
    public interface IEeStackOutputProcessor
    {
        EeStackReport ProcessOutput(string eeStackOutput);
    }
}