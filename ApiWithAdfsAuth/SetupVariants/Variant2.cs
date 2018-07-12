using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.WsFederation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ApiWithAdfsAuth.SetupVariants
{
    public class Variant2: ISetupVariant
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

                wsFederationOptions.Events = new CookieWithSTSTokenMatchingAuthEventsWsFederationEvents();
            })

            .AddCookie(cookieOptions =>
            {
                cookieOptions.Events = new CookieWithSTSTokenMatchingAuthEvents();
            });

            return services;
        }
    }

    public sealed class CookieWithSTSTokenMatchingAuthEventsWsFederationEvents : DebugWsFederationEvents
    {
        public override Task SecurityTokenValidated(SecurityTokenValidatedContext context)
        {
            context.Response.Headers["STS_TOKEN_VALID_TO"] = context.SecurityToken.ValidTo.ToString("s", CultureInfo.InvariantCulture);
            return base.SecurityTokenValidated(context);
        }
    }

    public sealed class CookieWithSTSTokenMatchingAuthEvents : DebugCookieAuthEvents
    {
        public override Task SigningIn(CookieSigningInContext context)
        {
            var stsTokenValidToText = context.Response.Headers["STS_TOKEN_VALID_TO"];
            var restoredDateTime = DateTime.ParseExact(stsTokenValidToText, "s", CultureInfo.InvariantCulture);
            var cookieExpirationDateTime = new DateTime(
                restoredDateTime.Year,
                restoredDateTime.Month,
                restoredDateTime.Day,
                restoredDateTime.Hour,
                restoredDateTime.Minute,
                restoredDateTime.Second,
                restoredDateTime.Millisecond,
                DateTimeKind.Utc
            );

            context.CookieOptions.Expires = new DateTimeOffset(cookieExpirationDateTime);
            return base.SigningIn(context);
        }
    }
}
