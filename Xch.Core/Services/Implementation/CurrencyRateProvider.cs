using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xch.Core.Model;

namespace Xch.Core.Services.Implementation
{
    public class CurrencyRateProvider : ICurrencyRateProvider
    {
        public string Uri { get; }
        private IBasicHttpWebRequestExecutor _webRequestExecutor;
        private ICurrencyRateDeserializer _deserializer;

        public CurrencyRateProvider(string uri, IBasicHttpWebRequestExecutor webRequestExecutor, ICurrencyRateDeserializer deserializer)
        {
            Uri = uri;
            _webRequestExecutor = webRequestExecutor;
            _deserializer = deserializer;
        }

        public async Task<CurrencyRatesSnapshot> GetCurrentRatesAsync()
        {
            await _webRequestExecutor.ExecuteAsync(Uri);
            var stream = _webRequestExecutor.GetResponseStream();
            var rates = await _deserializer.DeserializeCurrencyRatesAsync(stream);
            
            // TODO: I was not able to find a proper spec for the XML format. Do we expect an ordered input? Lets go for sure for now and sort it!
            return rates.OrderBy(r => r.Date).Last();
        }

        public Task<IEnumerable<CurrencyRatesSnapshot>> GetHistory(DateTime? minDate, DateTime? maxDate)
        {
            throw new NotImplementedException();
        }
    }
}