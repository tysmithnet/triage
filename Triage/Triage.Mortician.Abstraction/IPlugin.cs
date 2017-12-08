using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triage.Mortician.Abstraction
{
    public interface IPlugin
    {
        Task Setup();
    }
}
