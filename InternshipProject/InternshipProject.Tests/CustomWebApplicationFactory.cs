using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions; 
using InternshipProject.Data;
using System.Linq;

namespace InternshipProject.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // delete all DbContext ve DbContextOptions 
            services.RemoveAll<DbContextOptions<InternPortalContext>>();
            services.RemoveAll<InternPortalContext>();
            //isolated serviceProvider for tests (InMemory provider)
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // new dbContext for inMemory db 
            services.AddDbContext<InternPortalContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting")
                       .UseInternalServiceProvider(serviceProvider); // fix for conflict 
            });
        });
    }
}
