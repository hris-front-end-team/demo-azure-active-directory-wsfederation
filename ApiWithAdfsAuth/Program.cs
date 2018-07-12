using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApiWithAdfsAuth.SetupVariants;

namespace ApiWithAdfsAuth
{
    public sealed class Program
    {
        public static void Main(string[] commandLineArguments)
        {
            WebHost
                .CreateDefaultBuilder(commandLineArguments)
                .UseStartup<Startup>()
                .Build()
                .Run();
        }
    }

    public sealed class Startup
    {
        public static IDictionary<string, ISetupVariant> setupVariants =
            new Dictionary<string, ISetupVariant>
            {
                { typeof(Variant1).Name, new Variant1() },
                { typeof(Variant2).Name, new Variant2() },
                { typeof(Variant3).Name, new Variant3() },
            };

        public readonly IHostingEnvironment HostingEnvironment;
        public readonly IConfiguration Configuration;

        public Startup(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            HostingEnvironment = hostingEnvironment;
            Configuration = configuration;
        }

        public void Configure(IApplicationBuilder applicationBuilder)
        {
            (
                HostingEnvironment.IsDevelopment() ?
                    applicationBuilder.UseDeveloperExceptionPage() :
                    applicationBuilder
            )
            .UseAuthentication()
            .UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            setupVariants[Configuration["VariantNameKey"]]
                .ConfigureServices(services)
                .AddMvc();
        }
    }
}
