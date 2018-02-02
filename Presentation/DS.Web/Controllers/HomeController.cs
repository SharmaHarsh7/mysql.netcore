using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;

namespace MvcMovie.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        // 
        // GET: /HelloWorld/
        [HttpGet("")]
        public IActionResult Index()
        {
            Log.Information("Home > Index");

            

            return View();
        }

        private Exception Exception(string v)
        {
            throw new NotImplementedException();
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}