using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xch.Model;
using Xch.Services;
using Xch.Web.Controllers;
using Xunit;

namespace Xch.Tests.Controllers
{
    public class CurrencyControllerTests
    {
        private readonly CurrencyController _controller;

        
        public CurrencyControllerTests()
        {
            Mock<ICurrencyRateProvider> providerMock = new Mock<ICurrencyRateProvider>();

            Task<CurrencyRatesSnapshot> getCurrentRates = Task<CurrencyRatesSnapshot>.FromResult(GetTestRates());

            providerMock.Setup(p => p.GetCurrentRatesAsync()).Returns(getCurrentRates);
            
            _controller = new CurrencyController(providerMock.Object);
        }

        private static CurrencyRatesSnapshot GetTestRates()
        {
            return new CurrencyRatesSnapshot(new DateTime(2017, 01, 01), new[]
            {
                new CurrencyRate("HUF", 300),
                new CurrencyRate("EUR", 1.0),
            });
        }

        private static T ExtractJsonResult<T>(IActionResult result)
        {
            return (T)((JsonResult)result).Value;
        }

        [Fact]
        public async Task Exchange()
        {
            var result = await _controller.Exchange(600, "HUF", "EUR");
            double val = ExtractJsonResult<double>(result);

            Assert.Equal(2, val);
        }

        [Fact]
        public async Task Codes()
        {
            var result = await _controller.Codes();

            var codes = ExtractJsonResult<IReadOnlyList<string>>(result);

            Assert.SetEqual(new[]{"HUF", "EUR"}, codes);
        }
    }
}