namespace Xch
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
