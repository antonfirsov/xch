using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xch.Core.Model;

namespace Xch.Core.Services.Implementation
{
    /// <summary>
    /// Builds an in-memory "database" for currency rates, by caching the results of an other provider.
    /// </summary>
    public class CachingCurrencyRateProvider : ICurrencyRateProvider
    {
        private readonly ICurrencyRateProvider _wrappedProvider;
        private readonly TimeSpan _timeoutInterval;

        private DateTime _lastUpdate = DateTime.MinValue;
        private CurrencyRatesSnapshot _currentRates = null;

        public CachingCurrencyRateProvider(ICurrencyRateProvider wrappedProvider, TimeSpan timeoutInterval)
        {
            _wrappedProvider = wrappedProvider;
            _timeoutInterval = timeoutInterval;
        }

        public async Task<CurrencyRatesSnapshot> GetCurrentRatesAsync()
        {
            await UpdateOnTimeout();
            return _currentRates;
        }

        private bool NeedUpdate => _currentRates == null || DateTime.Now - _lastUpdate > _timeoutInterval;

        private async Task UpdateOnTimeout()
        {
            if (NeedUpdate)
            {
                _lastUpdate = DateTime.Now;
                _currentRates = await _wrappedProvider.GetCurrentRatesAsync();
            }
        }

        public Task<IEnumerable<CurrencyRatesSnapshot>> GetHistory(DateTime? minDate, DateTime? maxDate)
        {
            throw new NotImplementedException();
        }
    }
}