using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using DS.Data;

namespace DS.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Environment")]
    public class EnvironmentController : Controller
    {
        public EnvironmentController(IOptions<DBSettings> settings)
        {
        }

        // GET: api/Environment
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET: api/Environment/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Environment
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Environment/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
