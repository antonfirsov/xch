using System;
using System.IO;
using System.Threading.Tasks;

namespace Xch.Services
{
    public interface IBasicHttpWebRequestExecutor : IDisposable
    {
        Task ExecuteAsync(string requestiUri);
        Stream GetResponseStream();
    }
}