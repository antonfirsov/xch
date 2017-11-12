using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xch.Core.Model;

namespace Xch.Core.Services
{
    public interface ICurrencyRateProvider
    {
        Task<CurrencyRatesSnapshot> GetCurrentRatesAsync();

        Task<IEnumerable<CurrencyRatesSnapshot>> GetHistory(DateTime? minDate, DateTime? maxDate);
    }
}