using Microsoft.AspNetCore.Mvc;
using Triage.Mortician.Repository;

namespace Triage.Mortician.WebUiAnalyzer
{
    public class BaseController : Controller
    {
        internal static DumpInformationRepository DumpInformationRepository { get; set; }
        internal static DumpObjectRepository DumpObjectRepository { get; set; }
        internal static DumpAppDomainRepository DumpAppDomainRepository { get; set; }
        internal static DumpModuleRepository DumpModuleRepository { get; set; }
        internal static DumpThreadRepository DumpThreadRepository { get; set; }
        internal static DumpTypeRepository DumpTypeRepository { get; set; }
        internal static EventHub EventHub { get; set; }
    }
}