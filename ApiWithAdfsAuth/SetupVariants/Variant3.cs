using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.WsFederation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace ApiWithAdfsAuth.SetupVariants
{
    public class Variant3: ISetupVariant
    {
        public IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = WsFederationDefaults.AuthenticationScheme;
            })

            .AddWsFederation(wsFederationOptions =>
            {
                wsFederationOptions.MetadataAddress = "https://login.microsoftonline.com/7faaba4b-aa38-42a3-b558-30dfb3ab8262/federationmetadata/2007-06/federationmetadata.xml";
                wsFederationOptions.Wtrealm = "https://soloydenkogmail.onmicrosoft.com/WebApp-WSFederation-DotNet";

                wsFederationOptions.UseTokenLifetime = false; // Enables fall back to cookie-driven expiration
                wsFederationOptions.Events = new DebugWsFederationEvents();
            })

            .AddCookie(cookieOptions =>
            {
                cookieOptions.SlidingExpiration = true; // Enables re-issuance of the Cookie (and therefore STS token) WHEN PROPER EVENT* HAPPENS
                cookieOptions.ExpireTimeSpan = new TimeSpan(0, 1, 0); // Use TimeSpan.MaxValue to represent quasi-infinite token expiration time

                cookieOptions.Events = new DebugCookieAuthEvents();
            });

            return services;
        }
    }
}
