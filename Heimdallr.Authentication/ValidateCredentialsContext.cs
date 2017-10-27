using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Heimdallr.Authentication
{
    public class ValidateCredentialsContext : ResultContext<DevelopmentAuthenticationOptions>
    {
        public ValidateCredentialsContext(HttpContext context, AuthenticationScheme scheme, DevelopmentAuthenticationOptions options)
            : base(context, scheme, options)
        {
        }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
    }
}
