using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xch.Model;
using Xch.Services;

namespace Xch.Web.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ICurrencyRateProvider _currencyRateProvider;

        public CurrencyController(ICurrencyRateProvider currencyRateProvider)
        {
            _currencyRateProvider = currencyRateProvider;
        }

        public async Task<IActionResult> Exchange(double sourceAmount, string sourceCurrencyCode, string destCurrencyCode)
        {
            if (string.IsNullOrEmpty(sourceCurrencyCode))
            {
                throw new Exception("sourceCurrencyCode can't be null");
            }
            if (string.IsNullOrEmpty(destCurrencyCode))
            {
                throw new Exception("destCurrencyCode can't be null");
            }

            var currentRates = await _currencyRateProvider.GetCurrentRatesAsync();
            double result = currentRates.Exchange(sourceAmount, sourceCurrencyCode, destCurrencyCode);
            
            return Json(result);
        }

        public async Task<IActionResult> Codes()
        {
            var currentRates = await _currencyRateProvider.GetCurrentRatesAsync();
            string[] codes = currentRates.Select(r => r.Code.Value).ToArray();
            return Json(codes);
        }

        public async Task<IActionResult> History()
        {
            var rates = await _currencyRateProvider.GetAllRatesAsync();
            CurrencyHistory history = CurrencyHistory.CreateFromSnapshots(rates);

            var result = new
            {
                codes = history.Codes.Select(c => c.Value).ToArray(),
                dates = history.Dates.Select(d => d.ToString("yyyy-MM-dd")).ToArray(),
                data = history.ToArray()
            };

            return Json(result);
        }

        public IActionResult Test2()
        {
            return Content(_currencyRateProvider.GetType().FullName);
        }
    }
}