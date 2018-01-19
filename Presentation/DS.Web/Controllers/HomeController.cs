using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers
{

    public class HomeController : Controller
    {
        // 
        // GET: /HelloWorld/

        public string Index()
        {

            throw new System.Exception("asdas");

            return "website is running...";
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}