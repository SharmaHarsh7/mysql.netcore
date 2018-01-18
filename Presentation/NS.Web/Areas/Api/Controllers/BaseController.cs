using Microsoft.AspNetCore.Mvc;
using DS.Core.Domain.ViewModel;
using System.Web;

namespace DS.Web.Areas.Api.Controllers
{

    public class BaseController : Controller
    {
        CurrentUserVM currentUserVM;

        public BaseController()
        {
            
        }
    }
}