using System;

namespace Xch.Core
{
    public interface IHelloService
    {
        string SayHello();
    }

    public class HelloService : IHelloService
    {
        public string SayHello()
        {
            return $"Hello from {this.GetType().FullName}!";
        }
    }
}
