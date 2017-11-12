using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xch.Core.Model;

namespace Xch.Core.Services
{
    public interface ICurrencyRateDeserializer
    {
        Task<IEnumerable<CurrencyRatesSnapshot>> DeserializeCurrencyRatesAsync(Stream stream);
    }
}