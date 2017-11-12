using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xch.Services;
using Xunit;

namespace Xch.Tests.Services
{
    public class BasicCurrencyRateProviderTests : IDisposable
    {
        private CurrencyRateProviderFixture _fixture;

        public BasicCurrencyRateProviderTests()
        {
            _fixture = new CurrencyRateProviderFixture();
        }
        
        [Fact]
        public async Task GetCurrentRatesAsync_ReturnsDeserializedResult()
        {
            ICurrencyRateProvider provider = _fixture.BasicProvider;

            // Act:
            var result = await provider.GetCurrentRatesAsync();

            // Assert:
            Assert.Equal(_fixture.ExpectedCurrentRates.Count, result.Count);
        }

        [Fact]
        public async Task GetCurrentRatesAsync_RunsWebRequest()
        {
            var webxMock = _fixture.WebRequestExecutorMock;
            ICurrencyRateProvider provider = _fixture.BasicProvider;
            // Act:
            await provider.GetCurrentRatesAsync();

            // Assert:
            webxMock.Verify(x => x.ExecuteAsync(_fixture.ExpectedUri), Times.Once);
            webxMock.Verify(x => x.GetResponseStream(), Times.Once);
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }
    }
}