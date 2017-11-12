using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Xch.Web.Controllers;
using Xunit;

namespace Xch.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public async void Test()
        {
            HomeController controller = new HomeController(null);

            // Act:
            var result = await controller.Test();

            var jsonResult = Assert.IsType<JsonResult>(result);

            IEnumerable<double> values = (IEnumerable<double>) jsonResult.Value;

            Assert.Equal(new []{1.0, 2.0, 3.0}, values);
        }
    }
}
