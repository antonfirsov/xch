using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Xch.Model
{
    public class CurrencyHistory : IReadOnlyCollection<CurrencyTimeSeries>
    {
        private Dictionary<CurrencyCode, CurrencyTimeSeries> _data;
        
        public int DataPointCount => Dates.Count;

        public IReadOnlyList<CurrencyCode> Codes { get; }

        public IReadOnlyList<DateTime> Dates { get; }

        public IEnumerator<CurrencyTimeSeries> GetEnumerator() => _data.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public int Count => _data.Count;

        private CurrencyHistory(Dictionary<CurrencyCode, CurrencyTimeSeries> data,
            IEnumerable<CurrencyCode> codes,
            IEnumerable<DateTime> dates)
        {
            _data = data;
            var cc = codes.ToArray();
            Array.Sort(cc);
            Codes = cc;
            Dates = dates.ToArray();
        }

        public static CurrencyHistory CreateFromSnapshots(IEnumerable<CurrencyRatesSnapshot> snapshots)
        {
            Dictionary<CurrencyCode, List<double>> data = new Dictionary<CurrencyCode, List<double>>();
            
            foreach (CurrencyRatesSnapshot snapshot in snapshots)
            {
                foreach (CurrencyRate rate in snapshot)
                {
                    if (rate.Code == CurrencyCode.Eur) continue;
                    
                    if (!data.TryGetValue(rate.Code, out List<double> l))
                    {
                        l = new List<double>();
                        data[rate.Code] = l;
                    }
                    l.Add(rate.Rate);
                }
            }

            var dates = snapshots.Select(s => s.Date);
            var codes =
                snapshots.FirstOrDefault()?.Where(s => s.Code != CurrencyCode.Eur).Select(s => s.Code) ??
                Enumerable.Empty<CurrencyCode>();

            if (data.Any(d => d.Value.Count != dates.Count()))
            {
                throw new InvalidOperationException("CurrencyHistory.CreateFromSnapshots(): inconsistent input!");
            }

            var dataSeries = data.ToDictionary(kv => kv.Key, kv => new CurrencyTimeSeries(kv.Key, kv.Value));

            return new CurrencyHistory(dataSeries, codes, dates);
        }

        public CurrencyTimeSeries this[CurrencyCode code] => _data[code];
    }
}