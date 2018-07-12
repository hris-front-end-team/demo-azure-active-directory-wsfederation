using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Newtonsoft.Json;
using System.Linq;

namespace ApiWithAdfsAuth
{
    public sealed class AuthTokenDecryptor
    {
        public string DecryptAuthCookieFrom(HttpResponse httpResponse, CookieAuthenticationOptions options)
        {
            var newCookies = ((HttpResponseHeaders)httpResponse.Headers).HeaderSetCookie;

            var authCookieLongString = newCookies
                .ToArray()
                .Single(cookieText => cookieText.StartsWith(options.Cookie.Name + "="));

            var authCookie = authCookieLongString
                .Split(';')
                [0]
                .Replace(options.Cookie.Name, "")
                .Substring(1);

            var unprotectedCookie = options.TicketDataFormat.Unprotect(authCookie);

            return string.Join(" | ", new[] {
                "Issued: " + Date.Format(unprotectedCookie.Properties.IssuedUtc),
                "Expires: " + Date.Format(unprotectedCookie.Properties.ExpiresUtc),
                "Properties: " + JsonConvert.SerializeObject(unprotectedCookie.Properties),
                "AuthenticationScheme: " + unprotectedCookie.AuthenticationScheme,
            });
        }
    }
}
