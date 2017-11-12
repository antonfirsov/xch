using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xch.Services;

namespace Xch.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICurrencyRateProvider _currencyRateProvider;

        public HomeController(ICurrencyRateProvider currencyRateProvider)
        {
            _currencyRateProvider = currencyRateProvider;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public Task<IActionResult> Test()
        {
            double[] data = {1.0, 2.0, 3.0};
            return Task<IActionResult>.FromResult((IActionResult)Json(data));
        }

        public IActionResult Test2()
        {
            return Content(_currencyRateProvider.GetType().FullName);
        }
    }
}