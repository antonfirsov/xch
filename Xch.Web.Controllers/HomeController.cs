using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Xch.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public Task<IActionResult> Test()
        {
            double[] data = {1.0, 2.0, 3.0};
            return Task<IActionResult>.FromResult((IActionResult)Json(data));
        }
    }
}