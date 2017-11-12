using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xch.Core.Model;

namespace Xch.Core.Services
{
    public class EcbXmlCurrencyRateDeserializer : ICurrencyRateDeserializer
    {
        public Task<IEnumerable<CurrencyRatesSnapshot>> DeserializeCurrencyRatesAsync(Stream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}