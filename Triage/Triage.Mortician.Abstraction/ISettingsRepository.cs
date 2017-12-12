namespace Triage.Mortician.Abstraction
{
    public interface ISettingsRepository
    {
        string Get(string key);
    }
}