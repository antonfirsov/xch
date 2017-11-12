using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xch.Model;
using Xch.Services;
using Xch.Services.Implementation;
using Xunit;

namespace Xch.Tests.Services
{
    public class EcbXmlCurrencyRateDeserializerTests
    {
        public const string SampleXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>

<gesmes:Envelope xmlns:gesmes=""http://www.gesmes.org/xml/2002-08-01""
                 xmlns=""http://www.ecb.int/vocabulary/2002-08-01/eurofxref"">
  <gesmes:subject>Reference rates</gesmes:subject>
  <gesmes:Sender>
    <gesmes:name>European Central Bank</gesmes:name>
  </gesmes:Sender>
  <Cube>
    <Cube time=""2017-11-10"">
      <Cube currency=""USD"" rate=""1.1654"" /><Cube currency=""JPY"" rate=""132.08"" /><Cube currency=""BGN"" rate=""1.9558"" />
      <Cube currency=""CZK"" rate=""25.543"" /><Cube currency=""DKK"" rate=""7.442"" /><Cube currency=""GBP"" rate=""0.8837"" />
      <Cube currency=""HUF"" rate=""312"" /><Cube currency=""PLN"" rate=""4.2308"" /><Cube currency=""RON"" rate=""4.6533"" />
      <Cube currency=""SEK"" rate=""9.743"" /><Cube currency=""CHF"" rate=""1.1591"" /><Cube currency=""NOK"" rate=""9.456"" />
      <Cube currency=""HRK"" rate=""7.5435"" /><Cube currency=""RUB"" rate=""69.0375"" /><Cube currency=""TRY"" rate=""4.5097"" />
      <Cube currency=""AUD"" rate=""1.5197"" /><Cube currency=""BRL"" rate=""3.8084"" /><Cube currency=""CAD"" rate=""1.4766"" />
      <Cube currency=""CNY"" rate=""7.7412"" /><Cube currency=""HKD"" rate=""9.093"" /><Cube currency=""IDR"" rate=""15785.34"" />
      <Cube currency=""ILS"" rate=""4.123"" /><Cube currency=""INR"" rate=""75.94"" /><Cube currency=""KRW"" rate=""1304.46"" />
      <Cube currency=""MXN"" rate=""22.2242"" /><Cube currency=""MYR"" rate=""4.8848"" /><Cube currency=""NZD"" rate=""1.678"" />
      <Cube currency=""PHP"" rate=""59.713"" /><Cube currency=""SGD"" rate=""1.5854"" /><Cube currency=""THB"" rate=""38.61"" />
      <Cube currency=""ZAR"" rate=""16.7585"" />
    </Cube>
    <Cube time=""2017-09-01"">
      <Cube currency=""USD"" rate=""1.192"" /><Cube currency=""JPY"" rate=""131.29"" /><Cube currency=""BGN"" rate=""1.9558"" />
      <Cube currency=""CZK"" rate=""26.077"" /><Cube currency=""DKK"" rate=""7.4378"" /><Cube currency=""GBP"" rate=""0.92075"" />
      <Cube currency=""HUF"" rate=""305.09"" /><Cube currency=""PLN"" rate=""4.2406"" /><Cube currency=""RON"" rate=""4.5963"" />
      <Cube currency=""SEK"" rate=""9.4778"" /><Cube currency=""CHF"" rate=""1.1441"" /><Cube currency=""NOK"" rate=""9.2555"" />
      <Cube currency=""HRK"" rate=""7.4215"" /><Cube currency=""RUB"" rate=""68.8223"" /><Cube currency=""TRY"" rate=""4.0981"" />
      <Cube currency=""AUD"" rate=""1.5021"" /><Cube currency=""BRL"" rate=""3.7423"" /><Cube currency=""CAD"" rate=""1.483"" />
      <Cube currency=""CNY"" rate=""7.8185"" /><Cube currency=""HKD"" rate=""9.3272"" /><Cube currency=""IDR"" rate=""15875.38"" />
      <Cube currency=""ILS"" rate=""4.2599"" /><Cube currency=""INR"" rate=""76.3275"" /><Cube currency=""KRW"" rate=""1335.36"" />
      <Cube currency=""MXN"" rate=""21.2608"" /><Cube currency=""MYR"" rate=""5.0904"" /><Cube currency=""NZD"" rate=""1.6638"" />
      <Cube currency=""PHP"" rate=""60.9"" /><Cube currency=""SGD"" rate=""1.6146"" /><Cube currency=""THB"" rate=""39.539"" />
      <Cube currency=""ZAR"" rate=""15.4185"" />
    </Cube>
  </Cube>
</gesmes:Envelope>
";

        public class DeserializeCurrencyRatesAsync
        {
            private Task<IEnumerable<CurrencyRatesSnapshot>> DeserializeTestDataAsync()
            {
                // Arrange:
                ICurrencyRateDeserializer deserializer = new EcbXmlCurrencyRateDeserializer();

                using (var stream = GetStreamFromString(SampleXml))
                {
                    // Act:
                    return deserializer.DeserializeCurrencyRatesAsync(stream);
                }
            }

            [Fact]
            public async Task CorrectNumberOfSnapshotsIsDeserialized()
            {
                // Arrange & Act:
                var result = await DeserializeTestDataAsync();

                // Assert:
                Xunit.Assert.Equal(2, result.Count());
            }

            [Fact]
            public async Task DatesAreCorrect()
            {
                // Arrange & Act:
                var result = await DeserializeTestDataAsync();

                // Assert:
                var orderedResult = result.OrderBy(s => s.Date).ToArray();

                var snapshot0 = orderedResult[0];
                var snapshot1 = orderedResult[1];

                Xunit.Assert.Equal(new DateTime(2017, 09, 01), snapshot0.Date);
                Xunit.Assert.Equal(new DateTime(2017, 11, 10), snapshot1.Date);
            }

            [Fact]
            public async Task AllCurrenciesAreDeserialized()
            {
                // Arrange & Act:
                var result = await DeserializeTestDataAsync();
                
                // Assert:

                var snapshot0 = result.First();
                var snapshot1 = result.Last();


                CurrencyCode[] expectedCodes =
                {
                    "USD", "JPY", "BGN", "CZK", "DKK", "GBP", "HUF", "PLN", "RON", "SEK", "CHF", "NOK", "HRK", "RUB", "TRY",
                    "AUD", "BRL", "CAD", "CNY", "HKD", "IDR", "ILS", "INR", "KRW", "MXN", "MYR", "NZD", "PHP", "SGD",
                    "THB", "ZAR"
                };

                Assert.SetEqual(snapshot0.Select(cr => cr.Code), expectedCodes);
                Assert.SetEqual(snapshot1.Select(cr => cr.Code), expectedCodes);
            }

            [Fact]
            public async Task RatesAreCorrect()
            {
                // Arrange & Act:
                var result = await DeserializeTestDataAsync();

                // Assert:
                var snapshot0 = result.OrderBy(s => s.Date).First();

                //< Cube currency = ""USD"" rate = ""1.192"" />< Cube currency = ""JPY"" rate = ""131.29"" />< Cube currency = ""BGN"" rate = ""1.9558"" />
                //< Cube currency = ""CZK"" rate = ""26.077"" />< Cube currency = ""DKK"" rate = ""7.4378"" />< Cube currency = ""GBP"" rate = ""0.92075"" />

                Assert.Equal(1.192, snapshot0["USD"].Rate);
                Assert.Equal(131.29, snapshot0["JPY"].Rate);
                Assert.Equal(26.077, snapshot0["CZK"].Rate);
                Assert.Equal(0.92075, snapshot0["GBP"].Rate);
            }
        }
        
        private static Stream GetStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
