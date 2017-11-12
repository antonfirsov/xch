using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xch.Core.Model;

namespace Xch.Core.Services.Implementation
{
    public class CurrencyRateProvider : ICurrencyRateProvider
    {
        private IBasicHttpWebRequestExecutor _webRequestExecutor;
        private ICurrencyRateDeserializer _deserializer;

        public CurrencyRateProvider(IBasicHttpWebRequestExecutor webRequestExecutor, ICurrencyRateDeserializer deserializer)
        {
            _webRequestExecutor = webRequestExecutor;
            _deserializer = deserializer;
        }

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