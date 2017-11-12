using System;

namespace Xch.Model
{
    /// <summary>
    /// Represents the rate of a given currency of <see cref="Code"/> compared to EUR.
    /// This class is immutable.
    /// </summary>
    public class CurrencyRate
    {
        public CurrencyRate(CurrencyCode code, double rate)
        {
            if (rate < 0)
            {
                throw new ArgumentException("rate can't be less than zero!", nameof(rate));
            }
            Code = code;
            Rate = rate;
        }

        public CurrencyCode Code { get; }

        /// <summary>
        /// Eur / *
        /// </summary>
        public double Rate { get; }

        public static CurrencyRate Eur { get; } = new CurrencyRate(CurrencyCode.Eur, 1.0);
    }
}
