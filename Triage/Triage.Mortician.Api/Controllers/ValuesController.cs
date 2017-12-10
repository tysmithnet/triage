using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician.Api.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ValuesController : ApiController
    {
        [Import]
        public IDumpObjectRepository DumpObjectRepository { get; set; }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return DumpObjectRepository.Get().Take(5).Select(x => x.FullTypeName);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
