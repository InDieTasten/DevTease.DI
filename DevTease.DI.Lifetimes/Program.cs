using Microsoft.Extensions.DependencyInjection;
using System;

namespace DevTease.DI.Lifetimes
{
    internal class Program
    {
        private interface IFoo
        {
            Guid GetInstance();
        }

        private interface IFooSingleton : IFoo
        {
        }

        private interface IFooScoped : IFoo
        {
        }

        private interface IFooTransient : IFoo
        {
        }

        private class Foo : IFoo, IFooSingleton, IFooScoped, IFooTransient
        {
            private Guid _instanceId = Guid.NewGuid();
            Guid IFoo.GetInstance() => _instanceId;
        }

        private static readonly IServiceCollection _services = new ServiceCollection();
        private static IServiceProvider _provider;
        private static void Main(string[] args)
        {
            _services.AddSingleton<IFooSingleton, Foo>();
            _services.AddScoped<IFooScoped, Foo>();
            _services.AddTransient<IFooTransient, Foo>();

            _provider = _services.BuildServiceProvider();

            ResolveAndPrint();
            ResolveAndPrint();
            ResolveAndPrint();

            Console.ReadKey(true);
        }

        static void ResolveAndPrint()
        {
            Console.WriteLine("Singleton: {0}",
                _provider.GetService<IFooSingleton>().GetInstance());

            Console.WriteLine("Scoped: {0}",
                _provider.GetService<IFooScoped>().GetInstance());

            Console.WriteLine("Transient: {0}" + Environment.NewLine,
                _provider.GetService<IFooTransient>().GetInstance());
        }
    }
}
