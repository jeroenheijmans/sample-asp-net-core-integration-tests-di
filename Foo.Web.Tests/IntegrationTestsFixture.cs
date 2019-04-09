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
                // See also: https://stackoverflow.com/q/55599360/419956
                servicesConfiguration.AddScoped<IBarService>(di
                    => new DecoratedBarService(di.GetRequiredService<BarService>()));
            });            
        }
    }
}
