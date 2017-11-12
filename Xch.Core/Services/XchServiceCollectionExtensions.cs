using System;
using Microsoft.Extensions.DependencyInjection;
using Xch.Services.Implementation;

namespace Xch.Services
{
    public static class XchServiceCollectionExtensions
    {
        public const string DefaultCurrencyServiceUri =
            "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml";

        public static void AddXch(this IServiceCollection services, string currencyServiceUri = DefaultCurrencyServiceUri) =>
            services.AddXch(currencyServiceUri, TimeSpan.FromHours(1));

        public static void AddXch(this IServiceCollection services, string currencyServiceUri, TimeSpan currencyCacheTimoutInterval)
        {
            Func<IBasicHttpWebRequestExecutor> webRequestFactory = () => new BasicHttpWebRequestExecutor();

            services.AddSingleton(webRequestFactory);

            ICurrencyRateDeserializer deserializer = new EcbXmlCurrencyRateDeserializer();
            services.AddSingleton(deserializer);

            var basicProvider = new BasicCurrencyRateProvider(currencyServiceUri, webRequestFactory, deserializer);
            ICurrencyRateProvider cachingProvider = new CachingCurrencyRateProvider(basicProvider, currencyCacheTimoutInterval, true);

            services.AddSingleton(cachingProvider);
        }
    }
}