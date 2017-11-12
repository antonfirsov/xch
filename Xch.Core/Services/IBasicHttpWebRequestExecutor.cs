using System.IO;
using System.Threading.Tasks;

namespace Xch.Core.Services
{
    internal interface IBasicHttpWebRequestExecutor
    {
        Task<Stream> GetResponseStreamAsync(string url);
    }
}