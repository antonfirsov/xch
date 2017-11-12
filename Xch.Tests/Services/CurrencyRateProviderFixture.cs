using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moq;
using Xch.Model;
using Xch.Services;
using Xch.Services.Implementation;

namespace Xch.Tests.Services
{
    public class CurrencyRateProviderFixture : IDisposable
    {
        private MemoryStream _dummyStream;

        public BasicCurrencyRateProvider BasicProvider { get; }

        public CurrencyRatesSnapshot ExpectedCurrentRates => new CurrencyRatesSnapshot(new DateTime(2018, 01, 01), new[]
        {
            new CurrencyRate("HUF", 800),
        });

        public Mock<IBasicHttpWebRequestExecutor> WebRequestExecutorMock { get; }

        public string ExpectedUri { get; } = "foo://lol.com";

        private Task<IEnumerable<CurrencyRatesSnapshot>> MakeResultTask()
        {
            CurrencyRatesSnapshot expectedRates0 = new CurrencyRatesSnapshot(new DateTime(1971, 01, 01), new[]
            {
                new CurrencyRate("HUF", 0.000001),
            });


            return Task<IEnumerable<CurrencyRatesSnapshot>>.FromResult(
                (IEnumerable<CurrencyRatesSnapshot>)new[] { expectedRates0, ExpectedCurrentRates }
            );
        }

        public CurrencyRateProviderFixture()
        {
            _dummyStream = new MemoryStream();

            WebRequestExecutorMock = new Mock<IBasicHttpWebRequestExecutor>();
            WebRequestExecutorMock.Setup(x => x.ExecuteAsync(It.IsAny<string>())).Returns(Task.CompletedTask);
            WebRequestExecutorMock.Setup(x => x.GetResponseStream()).Returns(_dummyStream);
            
            Mock<ICurrencyRateDeserializer> deserializerMock = new Mock<ICurrencyRateDeserializer>();
            deserializerMock.Setup(d => d.DeserializeCurrencyRatesAsync(_dummyStream)).Returns(MakeResultTask);

            BasicProvider = new BasicCurrencyRateProvider(ExpectedUri, () => WebRequestExecutorMock.Object, deserializerMock.Object);
        }

        public void Dispose()
        {
            _dummyStream?.Dispose();
        }
    }
}