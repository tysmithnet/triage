using System;

namespace Triage.Mortician
{
    internal interface IConverterCache
    {
        bool Contains(object instance);
        T GetOrAdd<T>(object instance, Func<T> factoryMethod, Action setupFunction = null);
    }
}