using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xch.Core.Model;

namespace Xch.Core.Services.Implementation
{
    public class EcbXmlCurrencyRateDeserializer : ICurrencyRateDeserializer
    {
        private static readonly XNamespace CubeNamespace =
            XNamespace.Get("http://www.ecb.int/vocabulary/2002-08-01/eurofxref");

        private static readonly XName CubeName = XName.Get("Cube", CubeNamespace.NamespaceName);

        private static CurrencyRate ConvertRate(XElement element)
        {
            string code = (string) element.Attribute("currency");
            double rate = (double) element.Attribute("rate");
            return new CurrencyRate(code, rate);
        }

        private static CurrencyRatesSnapshot ConvertSnapshot(XElement element)
        {
            string dateStr = (string) element.Attribute("time");
            var date = DateTime.ParseExact(dateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var rates = element.Elements(CubeName).Select(ConvertRate);
            return new CurrencyRatesSnapshot(date, rates);
        }

        public async Task<IEnumerable<CurrencyRatesSnapshot>> DeserializeCurrencyRatesAsync(Stream stream)
        {
            // TODO: Could be optimized with XmlReader

            var reader = new StreamReader(stream);
            string xmlText = await reader.ReadToEndAsync();
            XDocument doc = XDocument.Parse(xmlText);

            if (doc.Root == null)
            {
                throw new Exception("EcbXmlCurrencyRateDeserializer: invalid xml!");
            }
            
            var snapshotElements = doc.Root.Elements(CubeName).Single().Elements(CubeName);

            return snapshotElements.Select(ConvertSnapshot);
        }
    }
}