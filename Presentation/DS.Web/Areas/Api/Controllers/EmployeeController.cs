using Microsoft.AspNetCore.Mvc;
using DS.Services;
using System.Linq;
using DS.Domain.Models.Users;
using DS.Core.Infrastructure;

namespace DS.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeesService;

        public EmployeeController(IEmployeeService employeesService)
        {
            _employeesService = employeesService;
        }

        // GET: api/Employee
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            var data = _employeesService.Queryable().ToList();

            //return Ok(data);

            return Ok(EngineContext.Current.Resolve<IUserService>().GetUser(1));
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
