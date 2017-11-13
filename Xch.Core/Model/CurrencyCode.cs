using System;
using System.Collections.Generic;

namespace Xch.Model
{
    /// <summary>
    /// Represents a 3 letter ISO 4217 currency code in a type-safe way.
    /// This struct is immutable.
    /// </summary>
    public struct CurrencyCode : IEquatable<CurrencyCode>, IComparable<CurrencyCode>
    {
        public string Value { get; }

        public CurrencyCode(string value)
        {
            if (value == null)
            {
                throw new ArgumentException("Value for CurrencyCode can't be null!", nameof(value));
            }

            if (value.Length != 3)
            {
                throw new ArgumentException($"{value}: Invalid currency!", nameof(value));
            }

            Value = value.ToUpper();
        }

        public bool Equals(CurrencyCode other)
        {
            return string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is CurrencyCode && Equals((CurrencyCode) obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(CurrencyCode left, CurrencyCode right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CurrencyCode left, CurrencyCode right)
        {
            return !left.Equals(right);
        }

        public static implicit operator CurrencyCode(string value) => new CurrencyCode(value);

        public override string ToString() => Value;

        public static readonly CurrencyCode Eur = "EUR";
        
        public int CompareTo(CurrencyCode other)
        {
            if (Equals(Eur) && !other.Equals(Eur))
            {
                return -1;
            }
            else if (other.Equals(Eur) && !Equals(Eur))
            {
                return 1;
            }

            return string.Compare(Value, other.Value, StringComparison.Ordinal);
        }
    }


}