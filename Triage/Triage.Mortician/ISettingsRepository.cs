namespace Triage.Mortician
{
    /// <summary>
    ///     Represents an object that is capable of getting the settings for this process
    /// </summary>
    public interface ISettingsRepository
    {
        /// <summary>
        ///     Gets the setting associated with the provided key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string Get(string key);
    }
}