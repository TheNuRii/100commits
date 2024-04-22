using Microsoft.Extensions.DependencyInjection;
using MySpot.Api.Services;
using Shouldly;
using Xunit;
namespace MySpot.Tests.Unit.Framework;

public class ServiceCollectionTests
{
    [Fact]
    public void test()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IMessenger, Messenger>();
        serviceCollection.AddScoped<IMessenger, Messenger2>();
        
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var messenger = serviceProvider.GetServices<IMessenger>();
    }

    private interface IMessenger
    {
        void Send();
    }

    private class Messenger : IMessenger
    {
        private readonly Guid _id = Guid.NewGuid();
        
        public void Send() => Console.WriteLine($"Sending a message ... [{_id}]");

    }
    
    private class Messenger2 : IMessenger
    {
        private readonly Guid _id = Guid.NewGuid();

        public void Send() => Console.WriteLine($"Sending a message ... [{_id}]");

    }
}

