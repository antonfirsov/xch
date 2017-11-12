namespace Xch.Core.Model
{
    /// <summary>
    /// Represents the rate of a given currency of <see cref="Code"/> compared to EUR.
    /// </summary>
    public class CurrencyRate
    {
        public CurrencyRate(CurrencyCode code, double rate)
        {
            Code = code;
            Rate = rate;
        }

        public CurrencyCode Code { get; }

        /// <summary>
        /// Eur / *
        /// </summary>
        public double Rate { get; }
    }
}
