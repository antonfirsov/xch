using Microsoft.AspNetCore.Mvc;

namespace Xch.Web.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}