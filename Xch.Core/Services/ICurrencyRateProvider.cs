using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xch.Core.Model;

namespace ChartSandbox.Services
{
    public interface ICurrencyRateProvider
    {
        Task<CurrencyRatesSnapshot> GetCurrentRatesAsync();

        Task<IEnumerable<CurrencyRatesSnapshot>> GetHistory(DateTime? minDate, DateTime? maxDate);
    }

    internal interface IBasicWebRequest
    {
        
    }

    internal class EcbXmlWebServiceCurrencyRateProvider : ICurrencyRateProvider
    {
        public EcbXmlWebServiceCurrencyRateProvider(string url)
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