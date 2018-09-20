using System;
using Triage.Mortician.Adapters;

namespace Triage.Mortician
{
    internal interface IConverterCache
    {
        T GetOrAdd<T>(object instance, Func<T> factoryMethod, Action setupFunction = null);
        bool Contains(object instance);
    }
}