namespace Triage.Mortician.Repository
{
    public interface ISettingsRepository
    {
        /// <summary>
        ///     Gets the setting associated with the provided key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        ///     Gets the boolean value associated with the provided key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="fallbackToDefault">if set to <c>true</c> use default(T).</param>
        /// <returns></returns>
        bool GetBool(string key, bool fallbackToDefault = false);
    }
}