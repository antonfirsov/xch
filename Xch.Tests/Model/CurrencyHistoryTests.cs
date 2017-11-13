using System;
using System.Linq;
using Xch.Model;
using Xunit;

namespace Xch.Tests.Model
{
    public class CurrencyHistoryTests
    {
        public class CreateFromTimeSeries
        {
            [Fact]
            public void ThrowsOnInconsistentInputCount()
            {
                CurrencyRate[] r1 =
                {
                    new CurrencyRate("HUF", 1)
                };

                CurrencyRate[] r2 =
                {
                    new CurrencyRate("HUF", 10), new CurrencyRate("FOO", 20)
                };

                CurrencyRatesSnapshot s1 = new CurrencyRatesSnapshot(DateTime.Now, r1);
                CurrencyRatesSnapshot s2 = new CurrencyRatesSnapshot(DateTime.Now, r2);

                Assert.ThrowsAny<Exception>(() =>
                {
                    CurrencyHistory.CreateFromSnapshots(new[] {s1, s2});
                });
            }

            [Fact]
            public void EurIsIgnored()
            {
                CurrencyRate[] r1 =
                {
                    new CurrencyRate("HUF", 1), new CurrencyRate("EUR", 2)
                };

                CurrencyRate[] r2 =
                {
                    new CurrencyRate("HUF", 10), new CurrencyRate("EUR", 20)
                };

                CurrencyRatesSnapshot s1 = new CurrencyRatesSnapshot(DateTime.Now, r1);
                CurrencyRatesSnapshot s2 = new CurrencyRatesSnapshot(DateTime.Now, r2);

                var history = CurrencyHistory.CreateFromSnapshots(new[] { s1, s2 });

                Assert.Equal(1, history.Count);
                Assert.Equal(1, history.Codes.Count);
                Assert.NotNull(history["huf"]);
            }
            
            private static CurrencyHistory CreateTestHistory()
            {
                DateTime d1 = new DateTime(2020, 01, 01);
                DateTime d2 = new DateTime(2020, 01, 02);
                DateTime d3 = new DateTime(2020, 01, 03);
                DateTime d4 = new DateTime(2020, 01, 04);

                CurrencyRate[] r1 =
                {
                    new CurrencyRate("HUF", 1), new CurrencyRate("FOO", 10), new CurrencyRate("BAR", 100)
                };

                CurrencyRate[] r2 =
                {
                    new CurrencyRate("HUF", 2), new CurrencyRate("FOO", 20), new CurrencyRate("BAR", 200)
                };

                CurrencyRate[] r3 =
                {
                    new CurrencyRate("HUF", 3), new CurrencyRate("FOO", 30), new CurrencyRate("BAR", 300)
                };

                CurrencyRate[] r4 =
                {
                    new CurrencyRate("HUF", 4), new CurrencyRate("FOO", 40), new CurrencyRate("BAR", 400)
                };

                CurrencyRatesSnapshot s1 = new CurrencyRatesSnapshot(d1, r1);
                CurrencyRatesSnapshot s2 = new CurrencyRatesSnapshot(d2, r2);
                CurrencyRatesSnapshot s3 = new CurrencyRatesSnapshot(d3, r3);
                CurrencyRatesSnapshot s4 = new CurrencyRatesSnapshot(d4, r4);

                return CurrencyHistory.CreateFromSnapshots(new[] {s1, s2, s3, s4});
            }

            [Fact]
            public void Count_IsCorrect()
            {
                // Arrange + Act:
                var history = CreateTestHistory();

                // Assert:
                Assert.Equal(3, history.Count);
            }

            [Fact]
            public void DataPointCount_IsCorrect()
            {
                // Arrange + Act:
                var history = CreateTestHistory();

                // Assert:
                Assert.Equal(4, history.DataPointCount);
            }

            [Fact]
            public void Codes_IsCorrect()
            {
                // Arrange + Act:
                var history = CreateTestHistory();

                // Assert:
                CurrencyCode[] expectedCodes = {"HUF", "FOO", "BAR"};
                Assert.SetEqual(expectedCodes, history.Codes);
            }

            [Fact]
            public void IndexerReturnsCorrectResult()
            {
                // Arrange + Act:
                var history = CreateTestHistory();

                // Assert:
                var huf = history["huf"];
                var foo = history["foo"];
                var bar = history["bar"];
                
                Assert.Equal("HUF", huf.CurrencyCode);
                Assert.Equal(
                    new[] {1.0, 2, 3, 4},
                    huf
                    );

                Assert.Equal("FOO", foo.CurrencyCode);
                Assert.Equal(
                    new[] { 10.0, 20, 30, 40 },
                    foo
                );

                Assert.Equal("BAR", bar.CurrencyCode);
                Assert.Equal(
                    new[] {100.0, 200, 300, 400},
                    bar
                    );
            }

            [Fact]
            public void Dates_IsCorrect()
            {
                // Arrange + Act:
                var history = CreateTestHistory();

                // Assert:
                DateTime d1 = new DateTime(2020, 01, 01);
                DateTime d2 = new DateTime(2020, 01, 02);
                DateTime d3 = new DateTime(2020, 01, 03);
                DateTime d4 = new DateTime(2020, 01, 04);

                Assert.Equal(new[]{d1, d2, d3, d4}, history.Dates);
            }
        }
    }
}