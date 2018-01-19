using Microsoft.AspNetCore.Mvc;
using DS.Services;
using System.Linq;
using DS.Domain.Models.Users;
using DS.Core.Infrastructure;
using DS.Services.Events;
using DS.Core;
using System;
using Microsoft.Extensions.Logging;

namespace DS.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeesService;
        private readonly IEventPublisher _publisher;
        private ILogger Logger { get; }

        public EmployeeController(IEmployeeService employeesService, IEventPublisher publisher, ILoggerFactory loggerFactory)
        {
            _employeesService = employeesService;
            _publisher = publisher;
            Logger = loggerFactory.CreateLogger(GetType().Namespace);
        }

        // GET: api/Employee
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            var data = _employeesService.Queryable().ToList();

            //return Ok(data);
            var userService = EngineContext.Current.Resolve<IUserService>();

            var a = userService.Queryable().FirstOrDefault();
            _publisher.EntityInserted(a);

            Logger.LogError("Sample Log");

            return Ok(data);
        }

        [HttpGet("{employeeId}")]
        public JsonResult Get(int employeeId)
        {

            var data = this._employeesService.Query().Select().Where(x => x.ID_Employee == employeeId);

            return Json(data);
        }


        // POST: api/Employee
        [HttpPost]
        public IActionResult Post([FromBody]Employee value)
        {
            return Ok(_employeesService.Insert(value));
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]Employee value)
        {
            return Ok(_employeesService.Update(value));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _employeesService.Delete(id);
        }
    }
}
