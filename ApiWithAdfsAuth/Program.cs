using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.WsFederation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddExperimentalConfiguration();
            services.AddMvc();
        }
    }

    public static class IServiceCollectionExtensions
    {
        public static AuthenticationBuilder AddExperimentalConfiguration(this IServiceCollection services)
        {
            // TODO: The following line is useless in our setup:  authOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            return services
                .AddAuthentication(authOptions =>
                {
                    authOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = WsFederationDefaults.AuthenticationScheme;
                })
                .AddExperimentalWsFederation()
                .AddExperimentalCookie();
        }

        public static AuthenticationBuilder AddExperimentalWsFederation(this AuthenticationBuilder services)
        {
            // TODO: wsFederationOptions.TokenValidationParameters is not even used...
            return services.AddWsFederation(wsFederationOptions =>
            {
                wsFederationOptions.MetadataAddress = "https://login.microsoftonline.com/7faaba4b-aa38-42a3-b558-30dfb3ab8262/federationmetadata/2007-06/federationmetadata.xml";
                wsFederationOptions.Wtrealm = "https://soloydenkogmail.onmicrosoft.com/WebApp-WSFederation-DotNet";

                // wsFederationOptions.UseTokenLifetime = false; // TODO Provide example

                wsFederationOptions.Events = new CustomWsFederationEvents();
            });
        }

        public static AuthenticationBuilder AddExperimentalCookie(this AuthenticationBuilder services)
        {
            // FIXME
            //   cookieOptions.Cookie.Expiration
            //   cookieOptions.Cookie.MaxAge
            //   cookieOptions.ExpireTimeSpan
            //   seem to have no effect on the AuthenticationCookie generated.
            //   Fine control is achievable via cookieOptions.Events.

            return services.AddCookie(cookieOptions => {
                //cookieOptions.SlidingExpiration = true; // TODO Check it's (ir)relevant
                cookieOptions.Events = new CustomCookieAuthenticationEvents();
            });
        }
    }
}
