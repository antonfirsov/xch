using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Xch.Core.Services.Implementation
{
    public class BasicHttpWebRequestExecutor : IBasicHttpWebRequestExecutor
    {
        private WebResponse _response = null;
        
        public async Task ExecuteAsync(string requestiUri)
        {
            if (_response != null)
            {
                throw new InvalidOperationException("BasicHttpWebRequestExecutor: Calling ExecuteAsync() more than once is invalid!");
            }
            WebRequest request = WebRequest.Create(requestiUri);
            _response = await request.GetResponseAsync();
        }

        public Stream GetResponseStream() => _response.GetResponseStream();

        public void Dispose()
        {
            _response?.Dispose();
            _response = null;
        }
    }
}