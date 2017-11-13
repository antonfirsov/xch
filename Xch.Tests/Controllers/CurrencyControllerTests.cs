using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Xch.Model;
using Xch.Services;
using Xch.Web.Controllers;
using Xunit;
using Xunit.Abstractions;

namespace Xch.Tests.Controllers
{
    public class CurrencyControllerTests
    {
        private readonly CurrencyController _controller;

        private ITestOutputHelper _output;
        
        public CurrencyControllerTests(ITestOutputHelper output)
        {
            _output = output;
            Mock<ICurrencyRateProvider> providerMock = new Mock<ICurrencyRateProvider>();

            Task<CurrencyRatesSnapshot> getCurrentRates = Task.FromResult(GetAllTestRates().Last());
            Task<IEnumerable<CurrencyRatesSnapshot>> getAllRates = Task.FromResult(GetAllTestRates());

            providerMock.Setup(p => p.GetCurrentRatesAsync()).Returns(getCurrentRates);
            providerMock.Setup(p => p.GetAllRatesAsync()).Returns(getAllRates);
            
            _controller = new CurrencyController(providerMock.Object);
        }

        private static IEnumerable<CurrencyRatesSnapshot> GetAllTestRates()
        {
            yield return new CurrencyRatesSnapshot(new DateTime(2017, 01, 01), new[]
            {
                new CurrencyRate("HUF", 301),
                new CurrencyRate("BAZ", 201),
                new CurrencyRate("EUR", 1.0),
            });

            yield return new CurrencyRatesSnapshot(new DateTime(2017, 01, 02), new[]
            {
                new CurrencyRate("HUF", 300),
                new CurrencyRate("BAZ", 200), 
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

            Assert.SetEqual(new[]{"HUF", "BAZ", "EUR" }, codes);
        }

        [Fact]
        public async Task History()
        {
            var result = await _controller.History();

            JsonResult jr = (JsonResult) result;

            dynamic history = ToDynamic(ExtractJsonResult<object>(result));
            
            string s = JsonConvert.SerializeObject(history);
            _output.WriteLine(s);

            Assert.Equal(new[] {"BAZ", "HUF"}, history.codes);
            Assert.Equal(new[] { "2017-01-01", "2017-01-02" }, history.dates);

            double[][] expectedData = {
                new []{201.0, 200.0},
                new []{301.0, 300.0}
            };

            Assert.Equal(expectedData, history.data);
        }

        private static dynamic ToDynamic(object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));

            return (ExpandoObject) expando;
        }
    }
}