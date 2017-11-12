using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xch.Core.Model;
using Xch.Core.Services;
using Xch.Core.Services.Implementation;
using Xunit;

namespace Xch.Tests.Services
{
    public class CachingCurrencyRateProviderTests : IDisposable
    {
        private CurrencyRateProviderFixture _fixture;

        public CachingCurrencyRateProviderTests()
        {
            _fixture = new CurrencyRateProviderFixture();
        }

        [Fact]
        public async Task ReturnsCorrectResult()
        {
            ICurrencyRateProvider wrappedProvider = _fixture.BasicProvider;

            CachingCurrencyRateProvider provider = new CachingCurrencyRateProvider(wrappedProvider, TimeSpan.FromHours(1));

            // Act:
            var result = await provider.GetCurrentRatesAsync();

            // Assert:
            Assert.Equal(_fixture.ExpectedCurrentRates.Count, result.Count);
        }

        [Fact]
        public async Task CacheIsUtilized()
        {
            ICurrencyRateProvider wrappedProvider = _fixture.BasicProvider;

            CachingCurrencyRateProvider provider = new CachingCurrencyRateProvider(wrappedProvider, TimeSpan.FromDays(1));

            // Act:
            var result0 = await provider.GetCurrentRatesAsync();
            var result1 = await provider.GetCurrentRatesAsync();

            // Assert:
            Assert.Same(result0, result1);
        }

        [Fact]
        public async Task CacheIsRefreshedAfterTimeout()
        {
            ICurrencyRateProvider wrappedProvider = _fixture.BasicProvider;

            CachingCurrencyRateProvider provider = new CachingCurrencyRateProvider(wrappedProvider, TimeSpan.FromMilliseconds(1));

            // Act:
            var result0 = await provider.GetCurrentRatesAsync();

            await Task.Delay(5);

            var result1 = await provider.GetCurrentRatesAsync();

            // Assert:
            Assert.NotSame(result0, result1);
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }
    }
   
}