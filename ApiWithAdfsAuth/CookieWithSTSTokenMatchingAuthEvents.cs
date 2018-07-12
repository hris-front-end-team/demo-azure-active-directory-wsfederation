using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ApiWithAdfsAuth
{
    public sealed class CookieWithSTSTokenMatchingAuthEvents : CookieAuthenticationEvents
    {
        public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            return base.RedirectToLogin(context);
        }

        public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            return base.ValidatePrincipal(context);
        }

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
