using System;
using System.Collections.Generic;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace IServiceProxySample
{
    public class TestServiceProxy<TService> : IServiceProxy
        where TService : IService
    {
        private readonly IDictionary<Type, Func<Uri, TService>> _serviceBuilders = new Dictionary<Type, Func<Uri, TService>>();

        public Type ServiceInterfaceType => typeof(TService);

        public Microsoft.ServiceFabric.Services.Remoting.V2.Client.IServiceRemotingPartitionClient ServicePartitionClient2 => throw new NotImplementedException();

        public Microsoft.ServiceFabric.Services.Remoting.V1.Client.IServiceRemotingPartitionClient ServicePartitionClient => throw new NotImplementedException();

        public TService Create(Type serviceType, Uri serviceUri, ServicePartitionKey partitionKey = null, TargetReplicaSelector targetReplicaSelector = TargetReplicaSelector.Default, string listenerName = null)
        {
            var serviceBuilder = _serviceBuilders[serviceType];
            return serviceBuilder(serviceUri);
        }

        public void AddServiceBuilder(Type serviceType, Func<Uri, TService> create)
        {
            _serviceBuilders[serviceType] = create;
        }
    }
}