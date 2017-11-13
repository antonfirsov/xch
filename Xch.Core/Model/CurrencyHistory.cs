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
            IReadOnlyList<CurrencyCode> codes,
            IReadOnlyList<DateTime> dates)
        {
            _data = data;
            Codes = codes;
            Dates = dates;
        }

        public static CurrencyHistory CreateFromSnapshots(IEnumerable<CurrencyRatesSnapshot> snapshots)
        {
            Dictionary<CurrencyCode, List<double>> data = new Dictionary<CurrencyCode, List<double>>();
            
            foreach (CurrencyRatesSnapshot snapshot in snapshots)
            {
                foreach (CurrencyRate rate in snapshot)
                {
                    //List<double> l;
                    if (!data.TryGetValue(rate.Code, out List<double> l))
                    {
                        l = new List<double>();
                        data[rate.Code] = l;
                    }
                    l.Add(rate.Rate);
                }
            }

            DateTime[] dates = snapshots.Select(s => s.Date).ToArray();
            CurrencyCode[] codes = snapshots.FirstOrDefault()?.Select(s => s.Code).ToArray() ?? new CurrencyCode[0];

            if (data.Any(d => d.Value.Count != dates.Length))
            {
                throw new InvalidOperationException("CurrencyHistory.CreateFromSnapshots(): inconsistent input!");
            }

            var dataSeries = data.ToDictionary(kv => kv.Key, kv => new CurrencyTimeSeries(kv.Key, kv.Value));

            return new CurrencyHistory(dataSeries, codes, dates);
        }

        public CurrencyTimeSeries this[CurrencyCode code] => _data[code];
    }
}