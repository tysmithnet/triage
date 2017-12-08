namespace Triage.Mortician.Abstraction
{
    public interface IDumpObjectRepository
    {
        IDumpObject Get(ulong address);           
    }
}