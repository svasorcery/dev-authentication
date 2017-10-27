using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;

namespace Heimdallr.Authentication
{
    public class AuthenticationFailedContext : ResultContext<DevelopmentAuthenticationOptions>
    {
        public AuthenticationFailedContext(HttpContext context, AuthenticationScheme scheme, DevelopmentAuthenticationOptions options)
            : base(context, scheme, options)
        {
        }

        public Exception Exception { get; set; }
    }
}
