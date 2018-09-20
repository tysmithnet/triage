using System;

namespace Triage.Mortician
{
    internal interface IConverterCache
    {
        T GetOrAdd<T>(object instance, Func<T> factoryMethod);
        bool Contains(object instance);
    }
}