using Microsoft.AspNetCore.Mvc;

namespace DS.Web.Controllers.Installer
{
    [Route("/installer/")]
    public class WelcomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {

            return View();
        }
    }
}