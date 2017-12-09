using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Triage.Mortician.Abstraction
{
    public interface IAnalyzer
    {
        Task Setup(CancellationToken cancellationToken);
        Task Process(CancellationToken cancellationToken);
    }        

    public interface ISignature
    {
        
    }

    public interface IReport
    {
        
    }
}
