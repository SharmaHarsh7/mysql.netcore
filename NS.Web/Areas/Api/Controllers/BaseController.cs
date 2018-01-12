using Microsoft.AspNetCore.Mvc;
using NS.Core.Domain.ViewModel;
using System.Web;

namespace NS.Web.Areas.Api.Controllers
{

    public class BaseController : Controller
    {
        CurrentUserVM currentUserVM;

        public BaseController()
        {
            
        }
    }
}