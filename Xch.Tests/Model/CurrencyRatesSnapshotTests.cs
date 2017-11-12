using System;
using System.Linq;
using Xch.Core.Model;
using Xunit;

namespace Xch.Tests.Model
{
    public class CurrencyRatesSnapshotTests
    {  
        [Fact]
        public void Constructor()
        {
            CurrencyRate[] testRates =
            {
                new CurrencyRate("HUF", 400),
                new CurrencyRate("USD", 0.8),
                new CurrencyRate("JPY", 133),
            };

            DateTime date = new DateTime(2018, 05, 20);

            CurrencyRatesSnapshot snapshot = new CurrencyRatesSnapshot(date, testRates);

            Assert.Equal(date, snapshot.Date);
            Assert.SetEqual(testRates.Select(r => r.Code), snapshot.Select(r => r.Code));
            Assert.Equal(400, snapshot["HUF"].Rate);
        }

    }
}