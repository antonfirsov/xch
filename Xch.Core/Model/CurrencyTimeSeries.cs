using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Xch.Model
{
    public class CurrencyTimeSeries : IReadOnlyList<double>
    {
        private readonly double[] _data;

        public CurrencyTimeSeries(CurrencyCode currencyCode, IEnumerable<double> data)
        {
            CurrencyCode = currencyCode;
            _data = data.ToArray();
        }

        public CurrencyCode CurrencyCode { get; }

        public IEnumerator<double> GetEnumerator()
        {
            IEnumerable<double> dataAsEnumerable = _data;
            return dataAsEnumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _data.Length;

        public double this[int index] => _data[index];
    }
}