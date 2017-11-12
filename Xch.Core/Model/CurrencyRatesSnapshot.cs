using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Xch.Core.Model
{
    /// <summary>
    /// Represents a collection of <see cref="Count"/> number of <see cref="CurrencyRate"/>-s at a given time (<see cref="Date"/>).
    /// The instances of this class are immutable.
    /// </summary>
    public class CurrencyRatesSnapshot : IReadOnlyCollection<CurrencyRate>
    {
        private Dictionary<CurrencyCode, CurrencyRate> _data;

        public DateTime Date { get; }

        public CurrencyRate this[CurrencyCode code]
        {
            get
            {
                if (!_data.TryGetValue(code, out var result))
                {
                    throw new ArgumentException("Unknown Currency:"+code, nameof(code));
                }
                return result;
            }
            
        } 

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

        public double Exchange(double sourceAmount, CurrencyCode sourceCurrencyCode, CurrencyCode destCurrencyCode)
        {
            CurrencyRate sr = this[sourceCurrencyCode];
            CurrencyRate dr = this[destCurrencyCode];

            double relativeRate = dr.Rate / sr.Rate;
            return sourceAmount * relativeRate;
        }

        /// <summary>
        /// Adds a rate for "EUR" to the snapshot to help calculations.
        /// </summary>
        public CurrencyRatesSnapshot AddEur()
        {
            CurrencyRatesSnapshot result = new CurrencyRatesSnapshot(Date, _data.Values);
            result._data[CurrencyCode.Eur] = CurrencyRate.Eur;
            return result;
        }
    }
}