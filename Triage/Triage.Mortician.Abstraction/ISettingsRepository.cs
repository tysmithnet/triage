using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triage.Mortician.Abstraction
{
    public interface ISettingsRepository
    {
        string Get(string key);
    }
}
