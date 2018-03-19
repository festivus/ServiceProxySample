using ServiceFabric.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServiceProxySample
{
    class Program
    {
        static void Main(string[] args)
        {
            SampleService service = new SampleService(MockStatelessServiceContextFactory.Default);
            var uri = new Uri("fabric:/SampleService");
            var factory = new TestServiceProxyFactory();
            factory.RegisterService(uri, service);
            var service2 = factory.CreateServiceProxy<ISampleService>(uri);
        }
    }
}
