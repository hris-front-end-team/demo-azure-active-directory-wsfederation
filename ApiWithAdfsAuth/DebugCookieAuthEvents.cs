using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ApiWithAdfsAuth
{
    public class DebugCookieAuthEvents : CookieAuthenticationEvents
    {
        public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            return base.RedirectToLogin(context);
        }

        public override Task RedirectToReturnUrl(RedirectContext<CookieAuthenticationOptions> context)
        {
            return base.RedirectToReturnUrl(context);
        }

        public override Task SigningIn(CookieSigningInContext context)
        {
            return base.SigningIn(context);
        }

        public override Task SignedIn(CookieSignedInContext context)
        {
            var decryptedCookieInfo = new AuthTokenDecryptor().DecryptAuthCookieFrom(context.Response, context.Options);
            var escapedDecryptedCookieInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(decryptedCookieInfo));
            context.Response.Cookies.Append($"ORIGINAL_SIGNIN_STS_TOKEN_BASE64", escapedDecryptedCookieInfo, new CookieOptions { Expires = DateTime.UtcNow.AddSeconds(1) });
            return base.SignedIn(context);
        }

        public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            return base.ValidatePrincipal(context);
        }
    }
}
