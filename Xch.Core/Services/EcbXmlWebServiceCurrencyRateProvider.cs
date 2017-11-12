using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xch.Core.Model;

namespace Xch.Core.Services
{
    internal class EcbXmlWebServiceCurrencyRateProvider : ICurrencyRateProvider
    {
        public EcbXmlWebServiceCurrencyRateProvider(string url, IBasicHttpWebRequestExecutor httpWebRequestExecutor)
        {
            Url = url;
        }

        public string Url { get; }
        public Task<CurrencyRatesSnapshot> GetCurrentRatesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CurrencyRatesSnapshot>> GetHistory(DateTime? minDate, DateTime? maxDate)
        {
            throw new NotImplementedException();
        }
    }
}