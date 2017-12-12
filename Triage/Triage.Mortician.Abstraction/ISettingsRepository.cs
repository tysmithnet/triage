namespace Triage.Mortician.Abstraction
{
    /// <summary>
    ///     Represents an object that is capable of getting the settings for this process
    /// </summary>
    public interface ISettingsRepository
    {
        string Get(string key);
    }
}