using System;
using System.Collections.Concurrent;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace IServiceProxySample
{
    public class TestServiceProxyFactory : IServiceProxyFactory
    {
        private readonly ConcurrentDictionary<Uri, IService> _serviceRegistry = new ConcurrentDictionary<Uri, IService>();

        public TServiceInterface CreateServiceProxy<TServiceInterface>(Uri serviceUri, ServicePartitionKey partitionKey = null, TargetReplicaSelector targetReplicaSelector = TargetReplicaSelector.Default, string listenerName = null)
            where TServiceInterface : IService
        {
            var serviceProxy = new TestServiceProxy<TServiceInterface>();
            serviceProxy.AddServiceBuilder(typeof(TServiceInterface), uri => (TServiceInterface)_serviceRegistry[uri]);
            return serviceProxy.Create(typeof(TServiceInterface), serviceUri, partitionKey, targetReplicaSelector, listenerName);
        }

        public void RegisterService(Uri serviceName, IService service)
        {
            _serviceRegistry.AddOrUpdate(serviceName, service, (name, svc) => service);
        }

    }
}