using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triage.Mortician.Core
{
    public class CodeLocation
    {
        public string Module { get; }
        public string Method { get; }
        public ulong Offset { get; }
        

        /// <inheritdoc />
        public CodeLocation(string module, string method, ulong offset)
        {
            Module = module;
            Method = method;
            Offset = offset;
        }
    }

    public class ManagedCodeLocation : CodeLocation
    {
        public ulong MethodDescriptor { get; }

        /// <inheritdoc />
        public ManagedCodeLocation(ulong methodDescriptor, ulong offset, string method) : base(null, method, offset)
        {
            MethodDescriptor = methodDescriptor;
        }
    }
}
