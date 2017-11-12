using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Xch.Core.Model
{
    /// <summary>
    /// Represents a collection of <see cref="Count"/> number of <see cref="CurrencyRate"/>-s at a given time (<see cref="Date"/>).
    /// </summary>
    public class CurrencyRatesSnapshot : IReadOnlyCollection<CurrencyRate>
    {
        private Dictionary<CurrencyCode, CurrencyRate> _data;

        public DateTime Date { get; }
        
        public CurrencyRate this[CurrencyCode code] => _data[code];

        public CurrencyRatesSnapshot(DateTime date, IEnumerable<CurrencyRate> rates)
        {
            Date = date;
            _data = rates.ToDictionary(r => r.Code, r => r);
        }

        public IEnumerator<CurrencyRate> GetEnumerator()
        {
            return _data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _data.Count;
    }
}