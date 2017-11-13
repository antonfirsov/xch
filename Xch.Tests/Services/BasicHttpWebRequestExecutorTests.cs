using System.IO;
using System.Threading.Tasks;
using Xch.Services;
using Xch.Services.Implementation;
using Xunit;
using Xunit.Abstractions;

namespace Xch.Tests.Services
{
    public class BasicHttpWebRequestExecutorTests
    {
        private ITestOutputHelper _output;

        public BasicHttpWebRequestExecutorTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact(Skip = "For local execution only.")]
        public async Task ExecuteAsync()
        {
            string uri = "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml";
            using (var executor = new BasicHttpWebRequestExecutor())
            {
                await executor.ExecuteAsync(uri);
                var stream = executor.GetResponseStream();
                using (StreamReader rdr = new StreamReader(stream))
                {
                    var result = await rdr.ReadToEndAsync();
                    _output.WriteLine(result);
                    Assert.NotEmpty(result);
                }
            }
        }
    }
}