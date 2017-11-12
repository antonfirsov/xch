using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xch.Core;
using Xunit;

namespace Xch.Tests
{
    public class HelloServiceTests
    {
        [Fact]
        public void SayHello_ResultIsNotEmpty()
        {
            HelloService svc = new HelloService();

            // Act:
            string hello = svc.SayHello();

            // Assert:
            Xunit.Assert.NotNull(hello);
            Xunit.Assert.NotEmpty(hello);
        }
    }
}
