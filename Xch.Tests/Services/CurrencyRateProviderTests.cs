using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xch.Core.Model;
using Xch.Core.Services;
using Xch.Core.Services.Implementation;
using Xunit;

namespace Xch.Tests.Services
{
    public class CurrencyRateProviderTests
    {
        [Fact]
        public async Task GetCurrentRatesAsync_ReturnsDeserializedResult()
        {
            using (var ms = new MemoryStream())
            {
                Mock<IBasicHttpWebRequestExecutor> webxMock = new Mock<IBasicHttpWebRequestExecutor>();
                webxMock.Setup(x => x.ExecuteAsync(It.IsAny<string>())).Returns(Task.CompletedTask);
                webxMock.Setup(x => x.GetResponseStream()).Returns(ms);

                CurrencyRatesSnapshot expectedRates0 = new CurrencyRatesSnapshot(new DateTime(1971, 01, 01), new[]
                {
                    new CurrencyRate("HUF", 0.000001),
                });
                CurrencyRatesSnapshot expectedRates1 = new CurrencyRatesSnapshot(new DateTime(2018, 01, 01), new[]
                {
                    new CurrencyRate("HUF", 800),
                });
                
                Mock<ICurrencyRateDeserializer> deserializerMock = new Mock<ICurrencyRateDeserializer>();
                var resultTask = Task<IEnumerable<CurrencyRatesSnapshot>>.FromResult(
                    (IEnumerable<CurrencyRatesSnapshot>) new[] {expectedRates0, expectedRates1}
                );

                deserializerMock.Setup(d => d.DeserializeCurrencyRatesAsync(ms)).Returns(resultTask);

                string url = "foo://lol.com";
                ICurrencyRateProvider provider =
                    new CurrencyRateProvider(url, webxMock.Object, deserializerMock.Object);

                // Act:
                var result = await provider.GetCurrentRatesAsync();

                // Assert:
                Assert.Equal(expectedRates1, result);
            }
        }

        [Fact]
        public async Task GetCurrentRatesAsync_RunsWebRequest()
        {
            using (var ms = new MemoryStream())
            {
                Mock<IBasicHttpWebRequestExecutor> webxMock = new Mock<IBasicHttpWebRequestExecutor>();
                webxMock.Setup(x => x.ExecuteAsync(It.IsAny<string>())).Returns(Task.CompletedTask);
                webxMock.Setup(x => x.GetResponseStream()).Returns(ms);

                CurrencyRatesSnapshot expectedRates0 = new CurrencyRatesSnapshot(new DateTime(1971, 01, 01), new[]
                {
                    new CurrencyRate("HUF", 0.000001),
                });

                Mock<ICurrencyRateDeserializer> deserializerMock = new Mock<ICurrencyRateDeserializer>();
                var resultTask = Task<IEnumerable<CurrencyRatesSnapshot>>.FromResult(
                    (IEnumerable<CurrencyRatesSnapshot>) new[] {expectedRates0}
                );

                deserializerMock.Setup(d => d.DeserializeCurrencyRatesAsync(ms)).Returns(resultTask);

                string uri = "foo://lol.com";
                ICurrencyRateProvider provider =
                    new CurrencyRateProvider(uri, webxMock.Object, deserializerMock.Object);

                // Act:
                await provider.GetCurrentRatesAsync();

                // Assert:
                webxMock.Verify(x => x.ExecuteAsync(uri), Times.Once);
                webxMock.Verify(x => x.GetResponseStream(), Times.Once);
            }
        }
    }
}