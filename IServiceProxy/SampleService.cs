using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServiceProxySample
{
    public class SampleService : StatelessService, ISampleService
    {
        public SampleService(StatelessServiceContext serviceContext) : base(serviceContext)
        {

        }

        public Task<string> GetString()
        {
            return Task.FromResult("SDFDSFDSFDSFSD");
        }
    }

    public interface ISampleService : IService
    {
        Task<string> GetString();
    }
}
