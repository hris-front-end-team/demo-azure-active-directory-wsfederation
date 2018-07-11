using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Threading.Tasks;

namespace ApiWithAdfsAuth
{
    public sealed class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
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
            context.CookieOptions.Expires = new DateTimeOffset(DateTime.UtcNow.AddMinutes(2));
            return base.SigningIn(context);
        }
    }
}
