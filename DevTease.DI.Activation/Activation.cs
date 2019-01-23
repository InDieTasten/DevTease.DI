using Microsoft.Extensions.DependencyInjection;
using System;

namespace DevTease.DI.Activation
{
    internal class Activation
    {
        private class Dependency
        {

        }

        private class ConsumerOnly
        {
            private readonly Dependency _dependency;

            public ConsumerOnly(Dependency dependency)
            {
                _dependency = dependency ?? throw new ArgumentNullException(nameof(dependency));
            }
        }


        private static readonly IServiceCollection _services = new ServiceCollection();
        private static IServiceProvider _provider;

        private static void Main(string[] args)
        {
            _services.AddTransient<Dependency>();

            _provider = _services.BuildServiceProvider();

            ConsumerOnly consumerOnly = ActivatorUtilities.CreateInstance<ConsumerOnly>(_provider)
                ?? throw new Exception("Creation of instance returned null");

            Console.ReadKey(true);
        }

    }
}
