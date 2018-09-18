using Microsoft.AspNetCore.Mvc;
using Triage.Mortician.Repository;

namespace Triage.Mortician.WebUiAnalyzer
{
    public class BaseController : Controller
    {
        internal static IDumpInformationRepository DumpInformationRepository { get; set; }
        internal static IDumpObjectRepository DumpObjectRepository { get; set; }
        internal static IDumpAppDomainRepository DumpAppDomainRepository { get; set; }
        internal static IDumpModuleRepository DumpModuleRepository { get; set; }
        internal static IDumpThreadRepository DumpThreadRepository { get; set; }
        internal static IDumpTypeRepository DumpTypeRepository { get; set; }
        internal static IEventHub EventHub { get; set; }
    }
}