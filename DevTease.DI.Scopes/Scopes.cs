using Microsoft.Extensions.DependencyInjection;
using System;

namespace DevTease.DI.Scopes
{
    internal static class Scopes
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


            _provider.ResolveAndPrint();
            _provider.ResolveAndPrint();
            _provider.ResolveAndPrint();

            using (IServiceScope serviceScope = _provider.CreateScope())
            {
                serviceScope.ServiceProvider.ResolveAndPrint();
                serviceScope.ServiceProvider.ResolveAndPrint();
                serviceScope.ServiceProvider.ResolveAndPrint();
            }

            using (IServiceScope serviceScope = _provider.CreateScope())
            {
                serviceScope.ServiceProvider.ResolveAndPrint();
                serviceScope.ServiceProvider.ResolveAndPrint();
                serviceScope.ServiceProvider.ResolveAndPrint();
            }

            Console.ReadKey(true);
        }

        private static void ResolveAndPrint(this IServiceProvider serviceProvider)
        {
            Console.WriteLine("Singleton: {0}",
                serviceProvider.GetService<IFooSingleton>().GetInstance());

            Console.WriteLine("Scoped: {0}",
                serviceProvider.GetService<IFooScoped>().GetInstance());

            Console.WriteLine("Transient: {0}" + Environment.NewLine,
                serviceProvider.GetService<IFooTransient>().GetInstance());
        }
    }
}
