using DS.Data.Mongo;
using DS.Framework.Mongo.Repository;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;

namespace MvcMovie.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IMongoDBContext _dBContext;

        public HomeController(IMongoDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        // 
        // GET: /HelloWorld/
        [HttpGet("")]
        public IActionResult Index()
        {
            return Ok(_dBContext.Test());
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