using System;
using System.Collections.Generic;
using System.Linq;
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
        private CurrencyRatesSnapshot[] _currentRates = null;

        public CachingCurrencyRateProvider(ICurrencyRateProvider wrappedProvider, TimeSpan timeoutInterval, bool autoAddEur = false)
        {
            _wrappedProvider = wrappedProvider;
            _timeoutInterval = timeoutInterval;
            _autoAddEur = autoAddEur;
        }

        public async Task<CurrencyRatesSnapshot> GetCurrentRatesAsync()
        {
            await UpdateOnTimeout();
            return _currentRates.Last();
        }

        private bool NeedUpdate => _currentRates == null || DateTime.Now - _lastUpdate > _timeoutInterval;

        private async Task UpdateOnTimeout()
        {
            if (NeedUpdate)
            {
                _lastUpdate = DateTime.Now;
                var rates = await _wrappedProvider.GetAllRatesAsync();
                _currentRates = rates.ToArray();
                if (_autoAddEur)
                {
                    for (int i = 0; i < _currentRates.Length; i++)
                    {
                        _currentRates[i] = _currentRates[i].AddEur();
                    }
                }
            }
        }

        public async Task<IEnumerable<CurrencyRatesSnapshot>> GetAllRatesAsync()
        {
            await UpdateOnTimeout();
            return _currentRates;
        }
    }
}