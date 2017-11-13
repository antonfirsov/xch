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

        public class CompareTo
        {

            [Theory]
            [InlineData("foo")]
            [InlineData("gbp")]
            [InlineData("aaa")]
            public void EurIsAlwaysFirst(string a)
            {
                var aa = new CurrencyCode(a);
                var eur0 = CurrencyCode.Eur;
                var eur1 = new CurrencyCode("eur");

                Assert.True(eur0.CompareTo(aa) < 0);
                Assert.True(eur1.CompareTo(aa) < 0);
            }

            [Fact]
            public void Eur2Eur()
            {
                var eur0 = CurrencyCode.Eur;
                var eur1 = new CurrencyCode("eur");

                Assert.Equal(0, eur0.CompareTo(eur0));
                Assert.Equal(0, eur0.CompareTo(eur1));
            }

            [Theory]
            [InlineData("aaa", "aaa", 0)]
            [InlineData("aaa", "aab", -1)]
            [InlineData("aab", "aaa", 1)]
            public void UsesAlphabeticalOrder(string a, string b, int expectedSign)
            {
                int res = new CurrencyCode(a).CompareTo(b);

                Assert.Equal(expectedSign, Math.Sign(res));
            }
        }

    }
}