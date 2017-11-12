using System;
using Xch.Model;
using Xunit;

namespace Xch.Tests.Model
{
    public class CurrencyCodeTests
    {
        [Theory]
        [InlineData("eur")]
        [InlineData("EuR")]
        [InlineData("foo")]
        public void Constructor_ShouldCreateUpperCaseCode(string codeValue)
        {
            CurrencyCode c = new CurrencyCode(codeValue);

            Assert.Equal(codeValue.ToUpper(), c.Value);
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("1234")]
        [InlineData("Arnold Schwarzenegger")]
        public void Constructor_ThrowsWhenCodeLengthIsNot3(string codeValue)
        {
            Assert.ThrowsAny<ArgumentException>(() => new CurrencyCode(codeValue));
        }

        [Theory]
        [InlineData("FOO", "FOO")]
        [InlineData("eur", "EUR")]
        public void Equality_WhenTrue(string c1, string c2)
        {
            CurrencyCode code1 = c1;
            CurrencyCode code2 = c2;

            Assert.Equal(code1, code2);
            Assert.Equal(code1.GetHashCode(), code2.GetHashCode());
        }

        [Theory]
        [InlineData("asd", "foo")]
        public void Equality_WhenFalse(string c1, string c2)
        {
            CurrencyCode code1 = c1;
            CurrencyCode code2 = c2;

            Assert.NotEqual(code1, code2);
        }
    }
}