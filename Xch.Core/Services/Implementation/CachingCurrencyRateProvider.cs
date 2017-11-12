using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xch.Model;

namespace Xch.Services.Implementation
{
    /// <summary>
    /// Builds an in-memory "database" for currency rates, by caching the results of an other provider.
    /// </summary>
    public class CachingCurrencyRateProvider : ICurrencyRateProvider
    {
        private readonly ICurrencyRateProvider _wrappedProvider;
        private readonly TimeSpan _timeoutInterval;
        private readonly bool _autoAddEur;

        private DateTime _lastUpdate = DateTime.MinValue;
        private CurrencyRatesSnapshot _currentRates = null;

        public CachingCurrencyRateProvider(ICurrencyRateProvider wrappedProvider, TimeSpan timeoutInterval, bool autoAddEur = false)
        {
            _wrappedProvider = wrappedProvider;
            _timeoutInterval = timeoutInterval;
            _autoAddEur = autoAddEur;
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
                if (_autoAddEur)
                {
                    _currentRates = _currentRates.AddEur();
                }
            }
        }

        public Task<IEnumerable<CurrencyRatesSnapshot>> GetHistory(DateTime? minDate, DateTime? maxDate)
        {
            throw new NotImplementedException();
        }
    }
}