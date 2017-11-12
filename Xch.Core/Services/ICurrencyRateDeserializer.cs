using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xch.Model;

namespace Xch.Services
{
    public interface ICurrencyRateDeserializer
    {
        Task<IEnumerable<CurrencyRatesSnapshot>> DeserializeCurrencyRatesAsync(Stream stream);
    }
}