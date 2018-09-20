using System;
using Triage.Mortician.Adapters;

namespace Triage.Mortician
{
    internal interface IConverterCache
    {
        T GetOrAdd<T>(object instance, Func<T> factoryMethod) where T : BaseAdapter;
        bool Contains(object instance);
    }
}