using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Xch.Services;
using Xch.Services.Implementation;
using Xch.Web.Controllers;
using Xunit;

namespace Xch.Tests.Services
{
    public class ServiceRegistrationTests
    {
        [Fact]
        public void AddXch_ShouldRegister_ICurrencyRateProvider()
        {
            ServiceCollection services = new ServiceCollection();

            // Act:
            services.AddXch();
            
            // Assert:
            var serviceProvider = new DefaultServiceProviderFactory().CreateServiceProvider(services);

            var provider = serviceProvider.GetService(typeof(ICurrencyRateProvider));

            Assert.IsType<CachingCurrencyRateProvider>(provider);
        }
    }
}