using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.WsFederation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ApiWithAdfsAuth.SetupVariants
{
    public class Variant1: ISetupVariant
    {
        public IServiceCollection ConfigureServices(IServiceCollection services)
        {
            // TODO: The following line is useless in our setup:  authOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = WsFederationDefaults.AuthenticationScheme;
            })

            // TODO: wsFederationOptions.TokenValidationParameters is not even used...
            .AddWsFederation(wsFederationOptions =>
            {
                wsFederationOptions.MetadataAddress = "https://login.microsoftonline.com/7faaba4b-aa38-42a3-b558-30dfb3ab8262/federationmetadata/2007-06/federationmetadata.xml";
                wsFederationOptions.Wtrealm = "https://soloydenkogmail.onmicrosoft.com/WebApp-WSFederation-DotNet";

                // wsFederationOptions.UseTokenLifetime = false;
                wsFederationOptions.Events = new DebugWsFederationEvents();
            })

            // FIXME
            //   cookieOptions.Cookie.Expiration
            //   cookieOptions.Cookie.MaxAge
            //   cookieOptions.ExpireTimeSpan
            //   seem to have no effect on the AuthenticationCookie generated.
            //   Fine control is achievable via cookieOptions.Events.
            .AddCookie(cookieOptions =>
            {
                //cookieOptions.SlidingExpiration = true; // TODO Check it's (ir)relevant
                cookieOptions.Events = new CookieWithFixedExpirationAuthEvents();
            });

            return services;
        }
    }

    public class CookieWithFixedExpirationAuthEvents : DebugCookieAuthEvents
    {
        public override Task SigningIn(CookieSigningInContext context)
        {
            context.CookieOptions.Expires = new DateTimeOffset(DateTime.UtcNow.AddMinutes(1));
            return base.SigningIn(context);
        }
    }
}
