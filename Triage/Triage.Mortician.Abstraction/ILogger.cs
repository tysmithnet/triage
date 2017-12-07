using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triage.Mortician.Abstraction
{
    public interface ILogger
    {
        void Write(string message);
    }

    public interface IRequiresLogger
    {
        void AcceptLogger(ILogger logger);
    }
}
