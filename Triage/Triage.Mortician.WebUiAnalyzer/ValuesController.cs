using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Triage.Mortician.WebUiAnalyzer
{
    /// <summary>
    ///     Class ValuesController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }
    }
}