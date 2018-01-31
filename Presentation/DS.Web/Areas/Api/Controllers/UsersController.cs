using Microsoft.AspNetCore.Mvc;
using DS.Domain.Models.Users;
using DS.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DS.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserService _usersService;

        public UserController(IUserService usersService)
        {
            _usersService = usersService;
        }

        // GET: api/User
        [HttpGet]
        [Route("")]
        [Authorize]
        public IActionResult Get()
        {
            var data = _usersService.Query().Include(x => x.Employees).Select();

            var userClaim = HttpContext.User.Claims.Where(x => x.Type == "UserName").FirstOrDefault();
            return Ok(userClaim?.Value ?? "No Value");
            return Ok(data);
        }

        [HttpGet("{userId}")]
        public IActionResult Get(int userId)
        {

            var data = this._usersService.Query().Select().Where(x => x.ID_User == userId);

            return Ok(data);
        }


        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User value)
        {
            return Ok(await _usersService.Insert(value));
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]User value)
        {
            return Ok(_usersService.Update(value));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _usersService.Delete(id);
        }
    }
}
