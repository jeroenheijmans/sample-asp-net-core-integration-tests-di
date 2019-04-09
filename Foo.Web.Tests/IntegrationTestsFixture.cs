using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Foo.Web.Tests
{
    public class IntegrationTestsFixture : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            
            builder.ConfigureTestServices(servicesConfiguration =>
            {
                // Solution for: https://stackoverflow.com/q/55599360/419956
                // See also other answers there, or in branches of the codebase.
                // The chosen solution here is adapted from the "Scrutor" NuGet package, which
                // is MIT licensed, and can be found at: https://github.com/khellang/Scrutor
                //
                // This solution might need further adaptation for thins like open generics...

                var descriptor = servicesConfiguration.Single(s => s.ServiceType == typeof(IBarService));

                servicesConfiguration.AddScoped<IBarService>(di 
                    => new DecoratedBarService(GetInstance<IBarService>(di, descriptor)));
            });
        }

        // Method loosely based on Scrutor, MIT licensed: https://github.com/khellang/Scrutor/blob/68787e28376c640589100f974a5b759444d955b3/src/Scrutor/ServiceCollectionExtensions.Decoration.cs#L319
        private static T GetInstance<T>(IServiceProvider provider, ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationInstance != null)
            {
                return (T)descriptor.ImplementationInstance;
            }

            if (descriptor.ImplementationType != null)
            {
                return (T)ActivatorUtilities.CreateInstance(provider, descriptor.ImplementationType);
            }

            if (descriptor.ImplementationFactory != null)
            {
                return (T)descriptor.ImplementationFactory(provider);
            }

            throw new InvalidOperationException($"Could not create instance for {descriptor.ServiceType}");
        }
    }
}
