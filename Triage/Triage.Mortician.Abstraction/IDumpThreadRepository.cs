namespace Triage.Mortician.Abstraction
{
    public interface IDumpThreadRepository
    {
        IDumpThread Get(uint osId);
    }
}