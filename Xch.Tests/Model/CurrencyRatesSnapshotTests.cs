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
            CurrencyRate[] inputRates =
            {
                new CurrencyRate("HUF", 400),
                new CurrencyRate("USD", 1.2),
                new CurrencyRate("JPY", 133),
            };
            
            DateTime date = new DateTime(2018, 05, 20);

            // Act:
            CurrencyRatesSnapshot snapshot = new CurrencyRatesSnapshot(date, inputRates);

            // Assert:
            Assert.Equal(date, snapshot.Date);
            Assert.SetEqual(inputRates.Select(r => r.Code), snapshot.Select(r => r.Code));
            Assert.Equal(400, snapshot["HUF"].Rate);
        }

        [Fact]
        public void Indexer_ThrowsOnUnknownCurrency()
        {
            var snapshot = CreateTestSnapshot();

            Assert.ThrowsAny<Exception>(() => snapshot["FOO"]);
        }
        
        [Fact]
        public void AddEur_CreatesNewSnapshotWithEur()
        {
            var snapshot0 = CreateTestSnapshot();

            // Act:
            var snapshot1 = snapshot0.AddEur();

            // Assert:

            Assert.NotSame(snapshot0, snapshot1);
            Assert.Equal(snapshot0.Count+1, snapshot1.Count);

            var eurRate = snapshot1[CurrencyCode.Eur];

            Assert.Equal(1.0, eurRate.Rate);
        }

        private static CurrencyRatesSnapshot CreateTestSnapshot()
        {
            CurrencyRate[] testRates =
            {
                new CurrencyRate("HUF", 400),
                new CurrencyRate("USD", 1.2),
                new CurrencyRate("GBP", 0.8),
            };

            return new CurrencyRatesSnapshot(new DateTime(2018, 05, 20), testRates);
        }

        public static TheoryData<double, string, string, double> ExchangeTestData = new TheoryData<double, string, string, double>()
            {
                {2, "HUF", "HUF", 2 },
                {42, "EUR", "EUR", 42 },
                {800, "HUF", "EUR", 2 },
                {2, "EUR", "HUF", 800 },
                {400, "HUF", "USD", 1.2 },
                {1.0, "GBP", "HUF", 500.0 }
            };

        [Theory]
        [MemberData(nameof(ExchangeTestData))]
        public void Exchange(double amount, string from, string to, double expected)
        {
            var snapshot = CreateTestSnapshot().AddEur();

            // Act:
            Assert.Equal(expected,
                snapshot.Exchange(amount, from, to)
                );
        }
    }
}