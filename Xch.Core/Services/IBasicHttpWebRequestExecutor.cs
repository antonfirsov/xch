using System;
using System.IO;
using System.Threading.Tasks;

namespace Xch.Core.Services
{
    public interface IBasicHttpWebRequestExecutor : IDisposable
    {
        Task ExecuteAsync(string requestiUri);
        Stream GetResponseStream();
    }
}