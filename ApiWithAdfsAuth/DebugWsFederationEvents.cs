using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.WsFederation;
using System.Threading.Tasks;

namespace ApiWithAdfsAuth
{
    public class DebugWsFederationEvents : WsFederationEvents
    {
        public override Task RedirectToIdentityProvider(RedirectContext context)
        {
            return base.RedirectToIdentityProvider(context);
        }

        public override Task SecurityTokenReceived(SecurityTokenReceivedContext context)
        {
            return base.SecurityTokenReceived(context);
        }

        public override Task SecurityTokenValidated(SecurityTokenValidatedContext context)
        {
            return base.SecurityTokenValidated(context);
        }

        public override Task TicketReceived(TicketReceivedContext context)
        {
            return base.TicketReceived(context);
        }

        public override Task RemoteSignOut(RemoteSignOutContext context)
        {
            return base.RemoteSignOut(context);
        }

        public override Task MessageReceived(MessageReceivedContext context)
        {
            return base.MessageReceived(context);
        }

        public override Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            return base.AuthenticationFailed(context);
        }

        public override Task RemoteFailure(RemoteFailureContext context)
        {
            return base.RemoteFailure(context);
        }
    }
}
