using Microsoft.Extensions.DependencyInjection;

namespace DevTease.DI.CollectionVsProvider
{
    internal class CollectionVsProvider
    {
        private class A : IA
        {

        }

        private interface IA
        {

        }

        private static void Main(string[] args)
        {
            // setting up the di
            var serviceCollection = new ServiceCollection();

            // registering the services
            serviceCollection.AddTransient<IA, A>();

            // create the provider
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            // retrieve service by abstraction
            IA instance = serviceProvider.GetRequiredService<IA>();

            // Conclusion
            // serviceCollection used for registering services.
            // serviceProvider used to retrieve service instances.
        }
    }
}
