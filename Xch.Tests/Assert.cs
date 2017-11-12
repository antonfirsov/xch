using System.Collections.Generic;

namespace Xch.Tests
{
    public class Assert : Xunit.Assert
    {
        public static void SetEqual<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            Assert.True(new HashSet<T>(a).SetEquals(b));
        }
    }
}