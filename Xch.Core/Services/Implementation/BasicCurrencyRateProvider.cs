using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xch.Core.Model;

namespace Xch.Core.Services.Implementation
{
    public class BasicCurrencyRateProvider : ICurrencyRateProvider
    {
        public string Uri { get; }
        private readonly Func<IBasicHttpWebRequestExecutor> _webRequestExecutorFactory;
        private readonly ICurrencyRateDeserializer _deserializer;

        public BasicCurrencyRateProvider(string uri, Func<IBasicHttpWebRequestExecutor> webRequestExecutorFactory, ICurrencyRateDeserializer deserializer)
        {
            Uri = uri;
            _webRequestExecutorFactory = webRequestExecutorFactory ?? throw new ArgumentNullException(nameof(webRequestExecutorFactory));
            _deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
        }
        
        public async Task<CurrencyRatesSnapshot> GetCurrentRatesAsync()
        {
            var executor = _webRequestExecutorFactory();
            await executor.ExecuteAsync(Uri);
            var stream = executor.GetResponseStream();
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